using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// The time-related input parameters for temporal data generators.
    /// </summary>
    public class TimeParameters : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the start time of the trading session.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DataType(DataType.Time)]
        [DefaultValue(DefaultParameterValues.SessionStartTimeString)]
        public TimeSpan SessionStartTime { get; set; } = DefaultParameterValues.SessionStartTime;

        /// <summary>
        /// Gets or sets the end time of the trading session.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DataType(DataType.Time)]
        [DefaultValue(DefaultParameterValues.SessionEndTimeString)]
        public TimeSpan SessionEndTime { get; set; } = DefaultParameterValues.SessionEndTime;

        /// <summary>
        /// Gets or sets the date of the first data sample.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [DefaultValue(DefaultParameterValues.StartDateString)]
        public DateTime StartDate { get; set; } = DefaultParameterValues.StartDate;

        /// <summary>
        /// Gets or sets the time granularity of data samples.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [EnumDataType(typeof(TimeGranularity), ErrorMessage = ErrorMessages.FieldEnumValueInvalid)]
        [DefaultValue(DefaultParameterValues.TimeGranularity)]
        public TimeGranularity TimeGranularity { get; set; } = DefaultParameterValues.TimeGranularity;

        /// <summary>
        /// Gets or sets a value specifying an exchange holiday schedule or a general country holiday schedule.
        /// Business days do not form a continuous sequence (weekends, holidays etc.),
        /// so there is a need in differentiating between the business time and the physical time.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired, AllowEmptyStrings = false)]
        [EnumDataType(typeof(BusinessDayCalendar), ErrorMessage = ErrorMessages.FieldEnumValueInvalid)]
        [DefaultValue(DefaultParameterValues.BusinessDayCalendar)]
        public BusinessDayCalendar BusinessDayCalendar { get; set; } = DefaultParameterValues.BusinessDayCalendar;

        /// <inheritdoc />
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SessionStartTime >= SessionEndTime)
            {
                yield return new ValidationResult(
                    $"{nameof(SessionStartTime)} {SessionStartTime} should be less than the {nameof(SessionEndTime)} {SessionEndTime}.",
                    new[] { nameof(SessionStartTime), nameof(SessionEndTime) });
            }
        }
    }
}
