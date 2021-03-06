using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Mbs.Trading.Instruments;
using Mbs.Trading.Time;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Historical
{
    /// <summary>
    /// Specifies parameters of data series to fetch.
    /// </summary>
    public class HistoricalDataRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalDataRequest"/> class.
        /// </summary>
        public HistoricalDataRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalDataRequest"/> class.
        /// </summary>
        /// <param name="instrument">The instrument of the time series.</param>
        /// <param name="startDate">The first date and time of the time series.</param>
        /// <param name="endDate">The last date and time of the time series.</param>
        /// <param name="timeGranularity">The time granularity of the time series.</param>
        /// <param name="endofdayClosingTime">Specifies a time-of-day to apply to the end-of-day dates during data fetching.</param>
        /// <param name="adjustedDataIfPresent">Use adjusted data if present.</param>
        public HistoricalDataRequest(
            Instrument instrument,
            DateTime startDate,
            DateTime endDate,
            TimeGranularity timeGranularity = TimeGranularity.Day1,
            TimeSpan? endofdayClosingTime = null,
            bool adjustedDataIfPresent = true)
        {
            Instrument = instrument;
            StartDate = startDate;
            EndDate = endDate;
            TimeGranularity = timeGranularity;
            AdjustedDataIfPresent = adjustedDataIfPresent;
            if (endofdayClosingTime.HasValue)
            {
                EndofdayClosingTime = endofdayClosingTime.Value;
            }
        }

        /// <summary>
        /// Gets or sets an instrument specifying the data series to fetch.
        /// </summary>
        [DataMember(IsRequired = true)]
        [Required(ErrorMessage = ValidationErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        public Instrument Instrument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to fetch an adjusted data series if possible.
        /// </summary>
        [DataMember(IsRequired = false)]
        [DefaultValue(true)]
        public bool AdjustedDataIfPresent { get; set; } = true;

        /// <summary>
        /// Gets if data is adjusted or null if unknown. This field is determined during fetching.
        /// </summary>
        public bool? IsDataAdjusted { get; internal set; }

        /// <summary>
        /// Gets or sets the time granularity of the time series.
        /// </summary>
        [DataMember(IsRequired = false)]
        [DefaultValue(TimeGranularity.Day1)]
        public TimeGranularity TimeGranularity { get; set; } = TimeGranularity.Day1;

        /// <summary>
        /// Gets or sets a time-of-day to apply to the end-of-day dates during data fetching if this time is not available in data.
        /// </summary>
        /// <remarks>
        /// Some data providers (such as Google) may overwrite this time with an actual one.
        /// </remarks>
        [DataMember(IsRequired = false)]
        [DataType(DataType.Time)]
        [DefaultValue("19:00:00")]
        public TimeSpan EndofdayClosingTime { get; set; } = new TimeSpan(19, 0, 0);

        /// <summary>
        /// Gets or sets the date of the first element of the time series.
        /// </summary>
        [DataMember(IsRequired = false)]
        [DataType(DataType.Date)]
        [DefaultValue("0001-01-01")]
        public DateTime StartDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the date of the last element of the time series.
        /// </summary>
        [DataMember(IsRequired = false)]
        [DataType(DataType.Date)]
        [DefaultValue("9999-12-31")]
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate <= StartDate)
            {
                yield return new ValidationResult(
                    "The EndDateTime should be greater than the StartDateTime.",
                    new[] { "StartDateTime", "EndDateTime" });
            }
        }
    }
}
