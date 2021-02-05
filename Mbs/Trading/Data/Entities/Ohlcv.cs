using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Entities
{
    /// <summary>
    /// An [open, high, low, close, volume] bar.
    /// </summary>
    [DataContract]
    public sealed class Ohlcv : TemporalEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ohlcv"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time of the closing price.</param>
        /// <param name="open">The opening price.</param>
        /// <param name="high">The highest price.</param>
        /// <param name="low">The lowest price.</param>
        /// <param name="close">The closing price.</param>
        /// <param name="volume">The volume.</param>
        public Ohlcv(DateTime dateTime, double open = double.NaN, double high = double.NaN, double low = double.NaN, double close = double.NaN, double volume = double.NaN)
            : base(dateTime)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ohlcv"/> class.
        /// </summary>
        public Ohlcv()
        {
        }

        /// <summary>
        /// Gets or sets the opening price.
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = ValidationErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public double Open { get; set; }

        /// <summary>
        /// Gets a value indicating whether the opening price is not initialized.
        /// </summary>
        public bool IsOpenEmpty => double.IsNaN(Open);

        /// <summary>
        /// Gets or sets the highest price.
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = ValidationErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public double High { get; set; }

        /// <summary>
        /// Gets a value indicating whether the highest price is not initialized.
        /// </summary>
        public bool IsHighEmpty => double.IsNaN(High);

        /// <summary>
        /// Gets or sets the lowest price.
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = ValidationErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public double Low { get; set; }

        /// <summary>
        /// Gets a value indicating whether the lowest price is not initialized.
        /// </summary>
        public bool IsLowEmpty => double.IsNaN(Low);

        /// <summary>
        /// Gets or sets the closing price.
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = ValidationErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public double Close { get; set; }

        /// <summary>
        /// Gets a value indicating whether the closing price is not initialized.
        /// </summary>
        public bool IsCloseEmpty => double.IsNaN(Close);

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = ValidationErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public double Volume { get; set; }

        /// <summary>
        /// Gets a value indicating whether the volume is not initialized.
        /// </summary>
        public bool IsVolumeEmpty => double.IsNaN(Volume);

        /// <summary>
        /// Gets a value indicating whether at least one of the price components is not initialized.
        /// </summary>
        public bool IsPriceEmpty => double.IsNaN(Open) || double.IsNaN(High) || double.IsNaN(Low) || double.IsNaN(Close);

        /// <summary>
        /// Gets the median price, calculated as (low + high) / 2.
        /// </summary>
        public double Median => IsMedianEmpty ? double.NaN : (Low + High) / 2;

        /// <summary>
        /// Gets a value indicating whether the median price is not initialized.
        /// </summary>
        public bool IsMedianEmpty => double.IsNaN(Low) || double.IsNaN(High);

        /// <summary>
        /// Gets the typical price, calculated as (low + high + close) / 3.
        /// </summary>
        public double Typical => (Low + High + Close) / 3;

        /// <summary>
        /// Gets a value indicating whether the typical price is not initialized.
        /// </summary>
        public bool IsTypicalEmpty => double.IsNaN(Close) || double.IsNaN(Low) || double.IsNaN(High);

        /// <summary>
        /// Gets the weighted price, calculated as (low + high + 2*close) / 4.
        /// </summary>
        public double Weighted => (Low + High + Close + Close) / 4;

        /// <summary>
        /// Gets a value indicating whether the weighted price is not initialized.
        /// </summary>
        public bool IsWeightedEmpty => double.IsNaN(Close) || double.IsNaN(Low) || double.IsNaN(High);

        /// <summary>
        /// Gets a value indicating whether this is a rising bar, i.e. the opening price is less than the closing price.
        /// </summary>
        public bool IsRising => Open < Close;

        /// <summary>
        /// Gets a value indicating whether this is a falling bar, i.e. the closing price is less than the opening price.
        /// </summary>
        public bool IsFalling => Open > Close;

        /// <summary>
        /// Gets a deep copy of this object.
        /// </summary>
        public override TemporalEntity Clone => new Ohlcv(Time, Open, High, Low, Close, Volume);

        /// <summary>
        /// Initializes a new instance of the <see cref="Ohlcv"/> class using the specified trade and the time granularity.
        /// </summary>
        /// <param name="trade">The trade.</param>
        /// <returns>The constructed object.</returns>
        public static Ohlcv CloneAggregation(Trade trade)
        {
            var value = trade.Price;
            return new Ohlcv(trade.Time, value, value, value, value, trade.Volume);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ohlcv"/> class using the specified scalar and the time granularity.
        /// </summary>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The constructed object.</returns>
        public static Ohlcv CloneAggregation(Scalar scalar)
        {
            var value = scalar.Value;
            return new Ohlcv(scalar.Time, value, value, value, value);
        }

        /// <summary>
        /// The value of the specified price type.
        /// </summary>
        /// <param name="priceType">The price type.</param>
        /// <returns>The value of the price.</returns>
        public double Price(OhlcvPriceType priceType)
        {
            return priceType switch
            {
                OhlcvPriceType.Closing => Close,
                OhlcvPriceType.Highest => High,
                OhlcvPriceType.Lowest => Low,
                OhlcvPriceType.Opening => Open,
                OhlcvPriceType.Median => Median,
                OhlcvPriceType.Typical => Typical,
                OhlcvPriceType.Weighted => Weighted,
                _ => double.NaN
            };
        }

        /// <summary>
        /// The value of the specified component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <returns>The value of the component.</returns>
        public double Component(OhlcvComponent component)
        {
            return component switch
            {
                OhlcvComponent.ClosingPrice => Close,
                OhlcvComponent.HighestPrice => High,
                OhlcvComponent.LowestPrice => Low,
                OhlcvComponent.OpeningPrice => Open,
                OhlcvComponent.MedianPrice => Median,
                OhlcvComponent.TypicalPrice => Typical,
                OhlcvComponent.WeightedPrice => Weighted,
                OhlcvComponent.Volume => Volume,
                _ => double.NaN
            };
        }

        /// <summary>
        /// Un-initializes the bar data; the date and time remain unchanged.
        /// </summary>
        public void Empty()
        {
            Open = double.NaN;
            High = double.NaN;
            Low = double.NaN;
            Close = double.NaN;
            Volume = double.NaN;
        }

        /// <summary>
        /// Makes a deep clone of the object applying the specified time granularity.
        /// </summary>
        /// <returns>The cloned object.</returns>
        public Ohlcv CloneAggregation()
        {
            return new Ohlcv(Time, Open, High, Low, Close, Volume);
        }

        /// <summary>
        /// Aggregates this bar with a next one in a continuous sequence of bars.
        /// The resulting date and time is the date and time of the last aggregated bar.
        /// </summary>
        /// <param name="other">A bar to aggregate with.</param>
        public void Aggregate(Ohlcv other)
        {
            Time = other.Time; // Always the closing time.
            Close = other.Close;
            if (IsPriceEmpty)
            {
                Open = other.Open;
                High = other.High;
                Low = other.Low;
                Volume = other.Volume;
            }
            else
            {
                Volume += other.Volume;
                if (High < other.High)
                {
                    High = other.High;
                }

                if (Low > other.Low)
                {
                    Low = other.Low;
                }
            }
        }

        /// <summary>
        /// Aggregates this bar with a next trade in a continuous sequence of trades.
        /// The resulting date and time is the date and time of the last aggregated trade.
        /// </summary>
        /// <param name="trade">A trade to aggregate with.</param>
        public void Aggregate(Trade trade)
        {
            var value = trade.Price;
            Time = trade.Time;
            Close = value;
            if (IsPriceEmpty)
            {
                Open = value;
                High = value;
                Low = value;
                Volume = trade.Volume;
            }
            else
            {
                Volume += trade.Volume;
                if (High < value)
                {
                    High = value;
                }

                if (Low > value)
                {
                    Low = value;
                }
            }
        }

        /// <summary>
        /// Aggregates this bar with a next scalar in a continuous sequence of scalars.
        /// The resulting date and time is the date and time of the last aggregated scalar.
        /// </summary>
        /// <param name="scalar">A scalar to aggregate with.</param>
        public void Aggregate(Scalar scalar)
        {
            var value = scalar.Value;
            Time = scalar.Time;
            Close = value;
            if (IsPriceEmpty)
            {
                Open = value;
                High = value;
                Low = value;
                Volume = double.NaN;
            }
            else
            {
                if (High < value)
                {
                    High = value;
                }

                if (Low > value)
                {
                    Low = value;
                }
            }
        }
    }
}
