using System;
using System.Runtime.Serialization;

namespace Mbs.Trading.Data
{
    /// <summary>
    /// A scalar value entity.
    /// </summary>
    [DataContract]
    public sealed class Scalar : TemporalEntity
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DataMember(Name = "value", IsRequired = true)]
        public double Value { get; set; }

        /// <summary>
        /// Gets a value indicating whether the scalar value is not initialized.
        /// </summary>
        public bool IsEmpty => double.IsNaN(Value);

        /// <summary>
        /// Uninitializes the scalar data; the date and time remain unchanged.
        /// </summary>
        public void Empty()
        {
            Value = double.NaN;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scalar"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="value">The value.</param>
        public Scalar(DateTime dateTime, double value = double.NaN)
            : base(dateTime)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scalar"/> class.
        /// </summary>
        public Scalar()
        {
        }

        /// <summary>
        /// Gets a deep copy of this object.
        /// </summary>
        public override TemporalEntity Clone => new Scalar(Time, Value);
    }
}
