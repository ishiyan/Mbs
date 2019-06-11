using System;
using System.Collections;
using System.Collections.Generic;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Timelines
{
    /// <summary>
    /// The merger.
    /// </summary>
    /// <typeparam name="T">The IComparable type.</typeparam>
    internal sealed class MergingEnumerable<T> : IEnumerable<MergingEnumerable<T>.Pair>
        where T : TemporalEntity
    {
        /// <summary>
        /// The value-action struct.
        /// </summary>
        public struct Pair
        {
            /// <summary>
            /// The value.
            /// </summary>
            internal readonly T Value;

            /// <summary>
            /// The action.
            /// </summary>
            internal readonly Action<T> Action;

            /// <summary>
            /// Initializes a new instance of the <see cref="Pair"/> struct.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="action">The action.</param>
            internal Pair(T value, Action<T> action)
            {
                Value = value;
                Action = action;
            }
        }

        private readonly List<Mergeable> mergeableList = new List<Mergeable>(1024);

        private class Mergeable : IComparable<Mergeable>
        {
            /// <summary>
            /// The comparer.
            /// </summary>
            private readonly IComparer<DateTime> comparer = Comparer<DateTime>.Default;

            /// <summary>
            /// The enumerable.
            /// </summary>
            internal readonly IEnumerable<T> Enumerable;

            /// <summary>
            /// The action.
            /// </summary>
            internal readonly Action<T> Action;

            /// <summary>
            /// The time granularity.
            /// </summary>
            internal readonly TimeGranularity TimeGranularity;

            /// <summary>
            /// The enumerator.
            /// </summary>
            internal IEnumerator<T> Enumerator;

            /// <summary>
            /// The ordinal number.
            /// </summary>
            internal long OrdinalNumber;

            /// <summary>
            /// Initializes a new instance of the <see cref="Mergeable"/> class.
            /// </summary>
            /// <param name="enumerable">The enumerable.</param>
            /// <param name="action">The action.</param>
            /// <param name="timeGranularity">The time granularity.</param>
            internal Mergeable(IEnumerable<T> enumerable, Action<T> action, TimeGranularity timeGranularity)
            {
                Enumerable = enumerable;
                Action = action;
                TimeGranularity = timeGranularity;
            }

            /// <summary>
            /// <see cref="IComparable{T}"/> implementation.
            /// </summary>
            /// <param name="other">The other instance to compare.</param>
            /// <returns>The result of comparison.</returns>
            public int CompareTo(Mergeable other)
            {
                object o = other;
                if (null == o)
                    return 1;
                if (other.Enumerator.Current == null)
                    return Enumerator.Current == null ? 0 : 1;
                if (Enumerator.Current == null)
                    return other.Enumerator.Current == null ? 0 : -1;
                int comparison = comparer.Compare(Enumerator.Current.Time, other.Enumerator.Current.Time);
                if (comparison == 0)
                    comparison = TimeGranularity.CompareTo(other.TimeGranularity);
                return comparison != 0 ? comparison :
                    OrdinalNumber > other.OrdinalNumber ? 1 :
                    OrdinalNumber < other.OrdinalNumber ? -1 : 0;
            }
        }

        /// <summary>
        /// Adds the enumerable to the merging conveyor.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        /// <param name="timeGranularity">The time granularity.</param>
        internal void Add(IEnumerable<T> enumerable, Action<T> action, TimeGranularity timeGranularity)
        {
            mergeableList.Add(new Mergeable(enumerable, action, timeGranularity));
        }

        /// <summary>
        /// Removes the enumerable from the merging conveyor.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        public void Remove(IEnumerable<T> enumerable)
        {
            mergeableList.RemoveAll(mergeable => enumerable.Equals(mergeable.Enumerable));
        }

        /// <summary>
        /// Gets the enumerator interface.
        /// </summary>
        /// <returns>The enumerator interface.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<Pair> GetEnumerator()
        {
            if (mergeableList.Count < 2)
            {
                switch (mergeableList.Count)
                {
                    case 1:
                        {
                            Action<T> action = mergeableList[0].Action;
                            foreach (var t in mergeableList[0].Enumerable)
                                yield return new Pair(t, action);
                        }

                        break;
                    default:
                        yield break;
                }
            }
            else
            {
                long nextOrdinalNumber = 0;
                var list = new List<Mergeable>(mergeableList);
                list.ForEach(mergeable =>
                {
                    mergeable.Enumerator = mergeable.Enumerable.GetEnumerator();
                    mergeable.Enumerator.MoveNext();
                    mergeable.OrdinalNumber = nextOrdinalNumber++;
                });
                list.Sort();
                while (list.Count != 0)
                {
                    Mergeable mergeable = list[0];
                    yield return new Pair(mergeable.Enumerator.Current, mergeable.Action);
                    list.RemoveAt(0);
                    if (mergeable.Enumerator.MoveNext())
                    {
                        mergeable.OrdinalNumber = nextOrdinalNumber++;
                        list.Add(mergeable);
                        if (1 < list.Count)
                            list.Sort();
                    }
                }
            }
        }
    }
}
