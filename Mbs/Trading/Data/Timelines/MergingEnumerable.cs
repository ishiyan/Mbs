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
        private readonly List<Mergeable> mergeableList = new List<Mergeable>(1024);

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
                            {
                                yield return new Pair(t, action);
                            }
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
                        if (list.Count > 1)
                        {
                            list.Sort();
                        }
                    }
                }
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
        /// The value-action struct.
        /// </summary>
        public readonly struct Pair
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

#pragma warning disable S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
        private class Mergeable : IComparable<Mergeable>
#pragma warning restore S1210
        {
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
            /// Gets the enumerable.
            /// </summary>
            internal IEnumerable<T> Enumerable { get; }

            /// <summary>
            /// Gets the action.
            /// </summary>
            internal Action<T> Action { get; }

            /// <summary>
            /// Gets the time granularity.
            /// </summary>
            internal TimeGranularity TimeGranularity { get; }

            /// <summary>
            /// Gets or sets the enumerator.
            /// </summary>
            internal IEnumerator<T> Enumerator { get; set; }

            /// <summary>
            /// Gets or sets the ordinal number.
            /// </summary>
            internal long OrdinalNumber { get; set; }

            /// <summary>
            /// Gets the comparer.
            /// </summary>
            private IComparer<DateTime> Comparer { get; } = Comparer<DateTime>.Default;

            /// <summary>
            /// <see cref="IComparable{T}"/> implementation.
            /// </summary>
            /// <param name="other">The other instance to compare.</param>
            /// <returns>The result of comparison.</returns>
            public int CompareTo(Mergeable other)
            {
                object o = other;
                if (o == null)
                {
                    return 1;
                }

                if (other.Enumerator.Current == null)
                {
                    return Enumerator.Current == null ? 0 : 1;
                }

                if (Enumerator.Current == null)
                {
                    return other.Enumerator.Current == null ? 0 : -1;
                }

                int comparison = Comparer.Compare(Enumerator.Current.Time, other.Enumerator.Current.Time);
                if (comparison == 0)
                {
                    comparison = TimeGranularity.CompareTo(other.TimeGranularity);
                }

                if (comparison != 0)
                {
                    return comparison;
                }

                long delta = OrdinalNumber - other.OrdinalNumber;
                if (delta > 0)
                {
                    return 1;
                }

                if (delta < 0)
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}
