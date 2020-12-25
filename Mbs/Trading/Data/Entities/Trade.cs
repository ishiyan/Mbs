using System;
using System.Runtime.Serialization;

// ReSharper disable once CheckNamespace
namespace Mbs.Trading.Data
{
    /// <summary>
    /// A trade (price and volume) entity.
    /// </summary>
    [DataContract]
    public sealed class Trade : TemporalEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trade"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="price">The price.</param>
        /// <param name="volume">The volume.</param>
        public Trade(DateTime dateTime, double price = double.NaN, double volume = double.NaN)
            : base(dateTime)
        {
            Price = price;
            Volume = volume;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trade"/> class.
        /// </summary>
        public Trade()
        {
        }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        [DataMember(Name = "price", IsRequired = true)]
        public double Price { get; set; }

        /// <summary>
        /// Gets a value indicating whether the price is not initialized.
        /// </summary>
        public bool IsPriceEmpty => double.IsNaN(Price);

        /// <summary>
        /// Gets or sets the volume (quantity).
        /// </summary>
        [DataMember(Name = "volume", IsRequired = true)]
        public double Volume { get; set; }

        /// <summary>
        /// Gets a value indicating whether the volume is not initialized.
        /// </summary>
        public bool IsVolumeEmpty => double.IsNaN(Volume);

        /// <summary>
        /// Gets a value indicating whether at least one of the components is not initialized.
        /// </summary>
        public bool IsEmpty => double.IsNaN(Price) || double.IsNaN(Volume);

        /// <summary>
        /// Gets a deep copy of this object.
        /// </summary>
        public override TemporalEntity Clone => new Trade(Time, Price, Volume);

        /// <summary>
        /// Un-initialize the trade data; the date and time remain unchanged.
        /// </summary>
        public void Empty()
        {
            Price = double.NaN;
            Volume = double.NaN;
        }
    }
}
