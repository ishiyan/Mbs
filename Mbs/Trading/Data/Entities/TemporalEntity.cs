using System;
using System.Runtime.Serialization;

namespace Mbs.Trading.Data
{
    /// <summary>
    /// An abstract temporal entity.
    /// </summary>
    /// <remarks>
    /// <see cref="IComparable{T}"/> is used in the <see cref="Mbs.Trading.Data.Timelines"/> implementation which uses <see cref="Mbs.Trading.Data.Timelines.MergingEnumerable{T}"/>.
    /// Two temporal entities are considered equal when they have the same time.
    /// </remarks>
    [DataContract]
    public abstract class TemporalEntity
    {
        /// <summary>
        /// Gets or sets the date and time. For <see cref="Ohlcv"/> bar entities it corresponds to the <see cref="Ohlcv.Close"/> time, so that an <see cref="Ohlcv"/> bar accumulates lower-level entities up to the closing date and time.
        /// </summary>
        [DataMember(Name = "time", IsRequired = true)]
        public DateTime Time { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporalEntity"/> class.
        /// </summary>
        protected TemporalEntity()
            : this(DateTime.Now)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemporalEntity"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        protected TemporalEntity(DateTime dateTime)
        {
            Time = dateTime;
        }

        /// <summary>
        /// Gets a deep copy of this object.
        /// </summary>
        public abstract TemporalEntity Clone { get; }
    }
}
