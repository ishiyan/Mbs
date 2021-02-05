using System;
using System.Runtime.Serialization;

namespace Mbs.Trading.Data.Entities
{
    /// <summary>
    /// A price quote (bid/ask price and size pair).
    /// </summary>
    [DataContract]
    public sealed class Quote : TemporalEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Quote"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time.</param>
        /// <param name="bidPrice">The bid price.</param>
        /// <param name="bidSize">The bid size.</param>
        /// <param name="askPrice">The ask price.</param>
        /// <param name="askSize">The ask size.</param>
        public Quote(DateTime dateTime, double bidPrice = double.NaN, double bidSize = double.NaN, double askPrice = double.NaN, double askSize = double.NaN)
            : base(dateTime)
        {
            BidPrice = bidPrice;
            BidSize = bidSize;
            AskPrice = askPrice;
            AskSize = askSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quote"/> class.
        /// </summary>
        public Quote()
        {
        }

        /// <summary>
        /// Gets or sets the bid price.
        /// </summary>
        [DataMember(Name = "bidPrice", IsRequired = true)]
        public double BidPrice { get; set; }

        /// <summary>
        /// Gets a value indicating whether a bid price is not initialized.
        /// </summary>
        public bool IsBidPriceEmpty => double.IsNaN(BidPrice);

        /// <summary>
        /// Gets or sets the bid size.
        /// </summary>
        [DataMember(Name = "bidSize", IsRequired = true)]
        public double BidSize { get; set; }

        /// <summary>
        /// Gets a value indicating whether a bid size is not initialized.
        /// </summary>
        public bool IsBidSizeEmpty => double.IsNaN(BidSize);

        /// <summary>
        /// Gets or sets the ask price.
        /// </summary>
        [DataMember(Name = "askPrice", IsRequired = true)]
        public double AskPrice { get; set; }

        /// <summary>
        /// Gets a value indicating whether an ask price is not initialized.
        /// </summary>
        public bool IsAskPriceEmpty => double.IsNaN(AskPrice);

        /// <summary>
        /// Gets or sets the ask size.
        /// </summary>
        [DataMember(Name = "askSize", IsRequired = true)]
        public double AskSize { get; set; }

        /// <summary>
        /// Gets a value indicating whether an ask size is not initialized.
        /// </summary>
        public bool IsAskSizeEmpty => double.IsNaN(AskSize);

        /// <summary>
        /// Gets a value indicating whether at least one of the components is not initialized.
        /// </summary>
        public bool IsEmpty => double.IsNaN(BidPrice) || double.IsNaN(BidSize) || double.IsNaN(AskPrice) || double.IsNaN(AskSize);

        /// <summary>
        /// Gets a deep copy of this object.
        /// </summary>
        public override TemporalEntity Clone => new Quote(Time, BidPrice, BidSize, AskPrice, AskSize);

        /// <summary>
        /// Un-initialize the quote data; the date and time remain unchanged.
        /// </summary>
        public void Empty()
        {
            BidPrice = double.NaN;
            BidSize = double.NaN;
            AskPrice = double.NaN;
            AskSize = double.NaN;
        }
    }
}
