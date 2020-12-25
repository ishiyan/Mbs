using System;
using Mbs.Trading.Holidays;
using Mbs.Trading.Markets;

namespace Mbs.Trading.Time.Conventions
{
    /// <summary>
    /// Business day calendar extensions.
    /// </summary>
    /// <remarks>
    /// Provides a specific exchange holiday schedule or a general country holiday schedule, which
    /// is used to determine whether a date is a business day, and to increment / decrement a date.
    /// </remarks>
    public static class BusinessDayCalendarExtensions
    {
        /// <summary>
        /// Given a business day calendar, returns a predicate to determine if a date is a business holiday.
        /// </summary>
        /// <param name="calendar">A business day calendar.</param>
        /// <returns>A predicate to determine if a date is a business holiday.</returns>
        public static Func<DateTime, bool> BusinessHolidayPredicate(this BusinessDayCalendar calendar)
        {
            return calendar switch
            {
                BusinessDayCalendar.Target => dateTime => dateTime.IsTargetHoliday(),
                BusinessDayCalendar.WeekendsOnly => IsWeekend,
                BusinessDayCalendar.NoHolidays => dateTime => false,
                BusinessDayCalendar.Euronext => dateTime => dateTime.IsEuronextHoliday(),
                BusinessDayCalendar.UnitedStates => dateTime => dateTime.IsUsHoliday(),
                BusinessDayCalendar.Switzerland => dateTime => dateTime.IsSwissHoliday(),
                BusinessDayCalendar.Sweden => dateTime => dateTime.IsSwedishHoliday(),
                BusinessDayCalendar.Denmark => dateTime => dateTime.IsDanishHoliday(),
                BusinessDayCalendar.Norway => dateTime => dateTime.IsNorwegianHoliday(),
                _ => IsWeekend
            };
        }

        /// <summary>
        /// Given a Market Identifier Code (MIC), returns a predicate to determine if a date is a business holiday for the related market.
        /// <para/>
        /// Verified using <c>http://www.marketholidays.com/HolidaysByCenter.aspx</c>.
        /// </summary>
        /// <param name="mic">An ISO 10383 Market Identifier Code (MIC).</param>
        /// <returns>A predicate to determine if a date is a business holiday for the related market.</returns>
        public static Func<DateTime, bool> BusinessHolidayPredicate(this ExchangeMic mic)
        {
            switch (mic)
            {
                case ExchangeMic.Xbru:
                case ExchangeMic.Alxb:
                case ExchangeMic.Enxb:
                case ExchangeMic.Mlxb:
                case ExchangeMic.Tnlb:
                case ExchangeMic.Vpxb:
                case ExchangeMic.Xbrd:
                case ExchangeMic.Xpar:
                case ExchangeMic.Alxp:
                case ExchangeMic.Xmat:
                case ExchangeMic.Xmli:
                case ExchangeMic.Xmon:
                case ExchangeMic.Xlis:
                case ExchangeMic.Alxl:
                case ExchangeMic.Enxl:
                case ExchangeMic.Mfox:
                case ExchangeMic.Wqxl:
                case ExchangeMic.Xams:
                case ExchangeMic.Tnla:
                case ExchangeMic.Xeuc:
                case ExchangeMic.Xeue:
                case ExchangeMic.Xeui:
                case ExchangeMic.Xldn:
                    return dateTime => dateTime.IsEuronextHoliday();
                case ExchangeMic.Edga:
                case ExchangeMic.Edgx:
                case ExchangeMic.Bato:
                case ExchangeMic.Bats:
                case ExchangeMic.Baty:
                case ExchangeMic.Bids:
                case ExchangeMic.Xbox:
                case ExchangeMic.C2Ox:
                case ExchangeMic.Xcbo:
                case ExchangeMic.Xcbf:
                case ExchangeMic.Fcbt:
                case ExchangeMic.Xcbt:
                case ExchangeMic.Xchi:
                case ExchangeMic.Fcme:
                case ExchangeMic.Xcme:
                case ExchangeMic.Xcec:
                case ExchangeMic.Edgo:
                case ExchangeMic.Eris:
                case ExchangeMic.Ifus:
                case ExchangeMic.Imfx:
                case ExchangeMic.Iexg:
                case ExchangeMic.Xisx:
                case ExchangeMic.Gmni:
                case ExchangeMic.Mcry:
                case ExchangeMic.Xkbt:
                case ExchangeMic.Xmio:
                case ExchangeMic.Xmge:
                case ExchangeMic.Gree:
                case ExchangeMic.Pipe:
                case ExchangeMic.Otcq:
                case ExchangeMic.Psgm:
                case ExchangeMic.Pinx:
                case ExchangeMic.Xpho:
                case ExchangeMic.Ootc:
                case ExchangeMic.Xotc:
                case ExchangeMic.Xoch:
                case ExchangeMic.Xnyl:
                case ExchangeMic.Nodx:
                case ExchangeMic.Xnym:
                case ExchangeMic.Xcis:
                case ExchangeMic.Xndq:
                case ExchangeMic.Xpsx:
                case ExchangeMic.Xphl:
                case ExchangeMic.Xbxo:
                case ExchangeMic.Xbos:
                case ExchangeMic.Xnms:
                case ExchangeMic.Xngs:
                case ExchangeMic.Xncm:
                case ExchangeMic.Xnas:
                case ExchangeMic.Amxo:
                case ExchangeMic.Xase:
                case ExchangeMic.Arco:
                case ExchangeMic.Arcx:
                case ExchangeMic.Xnys:
                    return dateTime => dateTime.IsUsHoliday();
                case ExchangeMic.Xbrn:
                case ExchangeMic.Aixe:
                case ExchangeMic.Xqmh:
                case ExchangeMic.Xswb:
                case ExchangeMic.Xswx:
                case ExchangeMic.Xvtx:
                    return dateTime => dateTime.IsSwissHoliday();
                case ExchangeMic.Xsto:
                case ExchangeMic.Nmtf:
                case ExchangeMic.Xngm:
                case ExchangeMic.Xndx:
                case ExchangeMic.Fnse:
                case ExchangeMic.Xsat:
                    return dateTime => dateTime.IsSwedishHoliday();
                case ExchangeMic.Dktc:
                case ExchangeMic.Xcse:
                case ExchangeMic.Fndk:
                    return dateTime => dateTime.IsDanishHoliday();
                case ExchangeMic.Xosl:
                case ExchangeMic.Xoas:
                case ExchangeMic.Notc:
                case ExchangeMic.Norx:
                    return dateTime => dateTime.IsNorwegianHoliday();
            }

            return IsWeekend;
        }

        /// <summary>
        /// If this date is not a business day (i.e., a holiday or a weekend) for the given market.
        /// </summary>
        /// <param name="dateTime">A date under question.</param>
        /// <param name="calendar">A business calendar providing holiday schedule.</param>
        /// <returns>A boolean representing if this date is not a business day for the given market.</returns>
        public static bool IsBusinessHoliday(this DateTime dateTime, BusinessDayCalendar calendar)
        {
            return calendar.BusinessHolidayPredicate()(dateTime);
        }

        /// <summary>
        /// If this date is not a business day (i.e., a holiday or a weekend) for the given market.
        /// </summary>
        /// <param name="dateTime">A date under question.</param>
        /// <param name="mic">An ISO 10383 Market Identifier Code (MIC).</param>
        /// <returns>A boolean representing if this date is not a business day for the given market.</returns>
        public static bool IsBusinessHoliday(this DateTime dateTime, ExchangeMic mic)
        {
            return mic.BusinessHolidayPredicate()(dateTime);
        }

        /// <summary>
        /// If this date is a business day for the given market.
        /// </summary>
        /// <param name="dateTime">A date under question.</param>
        /// <param name="calendar">A business calendar providing holiday schedule.</param>
        /// <returns>A boolean representing if this date is a business day for the given market.</returns>
        public static bool IsBusinessDay(this DateTime dateTime, BusinessDayCalendar calendar)
        {
            return !dateTime.IsBusinessHoliday(calendar);
        }

        /// <summary>
        /// If this date is a business day for the given market.
        /// </summary>
        /// <param name="dateTime">A date under question.</param>
        /// <param name="mic">An ISO 10383 Market Identifier Code (MIC).</param>
        /// <returns>A boolean representing if this date is a business day for the given market.</returns>
        public static bool IsBusinessDay(this DateTime dateTime, ExchangeMic mic)
        {
            return !dateTime.IsBusinessHoliday(mic);
        }

        /// <summary>
        /// Adjusts a non-business day to the appropriate near business day with respect to the given convention.
        /// </summary>
        /// <param name="dateTime">A date to adjust.</param>
        /// <param name="calendar">A business calendar providing holiday schedule.</param>
        /// <param name="convention">A convention of adjusting payment dates.</param>
        /// <returns>The adjusted date.</returns>
        public static DateTime AdjustToBusinessDay(this DateTime dateTime, BusinessDayCalendar calendar, BusinessDayConvention convention = BusinessDayConvention.Following)
        {
            Func<DateTime, bool> predicate = calendar.BusinessHolidayPredicate();
            int month;
            switch (convention)
            {
                case BusinessDayConvention.Following:
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(1d);
                    }

                    break;

                case BusinessDayConvention.ModifiedFollowing:
                    month = dateTime.Month;
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(1d);
                    }

                    if (month != dateTime.Month)
                    {
                        while (predicate(dateTime))
                        {
                            dateTime = dateTime.AddDays(-1d);
                        }
                    }

                    break;

                case BusinessDayConvention.Preceding:
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(-1d);
                    }

                    break;

                case BusinessDayConvention.ModifiedPreceding:
                    month = dateTime.Month;
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(-1d);
                    }

                    if (month != dateTime.Month)
                    {
                        while (predicate(dateTime))
                        {
                            dateTime = dateTime.AddDays(1d);
                        }
                    }

                    break;

                case BusinessDayConvention.Unadjusted:
                    break;
            }

            return dateTime;
        }

        /// <summary>
        /// Adjusts a non-business day to the appropriate near business day with respect to the given convention.
        /// </summary>
        /// <param name="dateTime">A date to adjust.</param>
        /// <param name="mic">An ISO 10383 Market Identifier Code (MIC).</param>
        /// <param name="convention">A convention of adjusting payment dates.</param>
        /// <returns>The adjusted date.</returns>
        public static DateTime AdjustToBusinessDay(this DateTime dateTime, ExchangeMic mic, BusinessDayConvention convention = BusinessDayConvention.Following)
        {
            Func<DateTime, bool> predicate = mic.BusinessHolidayPredicate();
            int month;
            switch (convention)
            {
                case BusinessDayConvention.Following:
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(1d);
                    }

                    break;

                case BusinessDayConvention.ModifiedFollowing:
                    month = dateTime.Month;
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(1d);
                    }

                    if (month != dateTime.Month)
                    {
                        while (predicate(dateTime))
                        {
                            dateTime = dateTime.AddDays(-1d);
                        }
                    }

                    break;

                case BusinessDayConvention.Preceding:
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(-1d);
                    }

                    break;

                case BusinessDayConvention.ModifiedPreceding:
                    month = dateTime.Month;
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(-1d);
                    }

                    if (month != dateTime.Month)
                    {
                        while (predicate(dateTime))
                        {
                            dateTime = dateTime.AddDays(1d);
                        }
                    }

                    break;

                case BusinessDayConvention.Unadjusted:
                    break;
            }

            return dateTime;
        }

        /// <summary>
        /// If the date is the last business day of the month in the given business day calendar.
        /// </summary>
        /// <param name="dateTime">A date in question.</param>
        /// <param name="calendar">A business calendar providing holiday schedule.</param>
        /// <param name="convention">A convention of adjusting payment dates.</param>
        /// <returns>Boolean indicating if this date is the last business day of the month.</returns>
        public static bool IsEndOfBusinessMonth(this DateTime dateTime, BusinessDayCalendar calendar, BusinessDayConvention convention = BusinessDayConvention.Following)
        {
            return dateTime.Month != dateTime.AddDays(1d).AdjustToBusinessDay(calendar, convention).Month;
        }

        /// <summary>
        /// If the date is the last business day of the month in the given market.
        /// </summary>
        /// <param name="dateTime">A date in question.</param>
        /// <param name="mic">An ISO 10383 Market Identifier Code (MIC).</param>
        /// <param name="convention">A convention of adjusting payment dates.</param>
        /// <returns>Boolean indicating if this date is the last business day of the month.</returns>
        public static bool IsEndOfBusinessMonth(this DateTime dateTime, ExchangeMic mic, BusinessDayConvention convention = BusinessDayConvention.Following)
        {
            return dateTime.Month != dateTime.AddDays(1d).AdjustToBusinessDay(mic, convention).Month;
        }

        /// <summary>
        /// If the date is a weekend (Saturday or Sunday) day.
        /// </summary>
        /// <param name="dateTime">A date in question.</param>
        /// <returns>Boolean indicating if this date is a weekend day.</returns>
        public static bool IsWeekend(this DateTime dateTime)
        {
            DayOfWeek dow = dateTime.DayOfWeek;
            return dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Advances the given date by the business date and time in the specified time span.
        /// </summary>
        /// <param name="dateTime">The date and time to advance.</param>
        /// <param name="timeSpan">The time span to advance the date and time.</param>
        /// <param name="calendar">A business calendar providing holiday schedule.</param>
        /// <returns>The advanced date and time.</returns>
        public static DateTime Advance(this DateTime dateTime, TimeSpan timeSpan, BusinessDayCalendar calendar = BusinessDayCalendar.Target)
        {
            double fractionalDays = timeSpan.TotalDays;
            var days = (int)fractionalDays;
            return dateTime.Advance(days, calendar).AddDays(fractionalDays - days);
        }

        /// <summary>
        /// Advances the given date by the business date and time in the specified time span.
        /// </summary>
        /// <param name="dateTime">The date and time to advance.</param>
        /// <param name="timeSpan">The time span to advance the date and time.</param>
        /// <param name="mic">An ISO 10383 Market Identifier Code (MIC).</param>
        /// <returns>The advanced date and time.</returns>
        public static DateTime Advance(this DateTime dateTime, TimeSpan timeSpan, ExchangeMic mic)
        {
            double fractionalDays = timeSpan.TotalDays;
            var days = (int)fractionalDays;
            return dateTime.Advance(days, mic).AddDays(fractionalDays - days);
        }

        /// <summary>
        /// Advances the given date by the specified number of business days.
        /// </summary>
        /// <param name="dateTime">The date and time to advance.</param>
        /// <param name="days">The number of days to advance the date and time.</param>
        /// <param name="calendar">A business calendar providing holiday schedule.</param>
        /// <returns>The advanced date and time.</returns>
        public static DateTime Advance(this DateTime dateTime, int days = 1, BusinessDayCalendar calendar = BusinessDayCalendar.Target)
        {
            Func<DateTime, bool> predicate = calendar.BusinessHolidayPredicate();
            if (days > 0)
            {
                while (days-- > 0)
                {
                    dateTime = dateTime.AddDays(1d);
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(1d);
                    }
                }
            }
            else
            {
                while (days++ < 0)
                {
                    dateTime = dateTime.AddDays(-1d);
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(-1d);
                    }
                }
            }

            return dateTime;
        }

        /// <summary>
        /// Advances the given date by the specified number of business days.
        /// </summary>
        /// <param name="dateTime">The date and time to advance.</param>
        /// <param name="days">The number of days to advance the date and time.</param>
        /// <param name="mic">An ISO 10383 Market Identifier Code (MIC).</param>
        /// <returns>The advanced date and time.</returns>
        public static DateTime Advance(this DateTime dateTime, int days, ExchangeMic mic)
        {
            Func<DateTime, bool> predicate = mic.BusinessHolidayPredicate();
            if (days > 0)
            {
                while (days-- > 0)
                {
                    dateTime = dateTime.AddDays(1d);
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(1d);
                    }
                }
            }
            else
            {
                while (days++ < 0)
                {
                    dateTime = dateTime.AddDays(-1d);
                    while (predicate(dateTime))
                    {
                        dateTime = dateTime.AddDays(-1d);
                    }
                }
            }

            return dateTime;
        }

        /// <summary>
        /// Calculates the number of business days between two given dates.
        /// </summary>
        /// <param name="first">The first date.</param>
        /// <param name="last">The last date.</param>
        /// <param name="calendar">A business calendar providing holiday schedule.</param>
        /// <param name="includeFirst">If the first date is inclusive.</param>
        /// <param name="includeLast">If the last date is inclusive.</param>
        /// <returns>The number of business dates.</returns>
        public static int BusinessDaysBetween(this DateTime first, DateTime last, BusinessDayCalendar calendar = BusinessDayCalendar.Target, bool includeFirst = true, bool includeLast = false)
        {
            Func<DateTime, bool> predicate = calendar.BusinessHolidayPredicate();
            int businessDays = 0;
            if (first != last)
            {
                if (first < last)
                {
                    for (DateTime date = first; date < last; date = date.AddDays(1d))
                    {
                        if (predicate(date))
                        {
                            ++businessDays;
                        }
                    }

                    if (predicate(last))
                    {
                        ++businessDays;
                    }
                }
                else
                {
                    for (DateTime date = last; date < first; date = date.AddDays(1d))
                    {
                        if (predicate(date))
                        {
                            ++businessDays;
                        }
                    }

                    if (predicate(first))
                    {
                        ++businessDays;
                    }
                }

                if (!includeFirst && predicate(first))
                {
                    --businessDays;
                }

                if (!includeLast && predicate(last))
                {
                    --businessDays;
                }

                if (first > last)
                {
                    businessDays = -businessDays;
                }
            }

            return businessDays;
        }

        /// <summary>
        /// Calculates the number of business days between two given dates.
        /// </summary>
        /// <param name="first">The first date.</param>
        /// <param name="last">The last date.</param>
        /// <param name="mic">An ISO 10383 Market Identifier Code (MIC).</param>
        /// <param name="includeFirst">If the first date is inclusive.</param>
        /// <param name="includeLast">If the last date is inclusive.</param>
        /// <returns>The number of business dates.</returns>
        public static int BusinessDaysBetween(this DateTime first, DateTime last, ExchangeMic mic, bool includeFirst = true, bool includeLast = false)
        {
            Func<DateTime, bool> predicate = mic.BusinessHolidayPredicate();
            int businessDays = 0;
            if (first != last)
            {
                if (first < last)
                {
                    for (DateTime date = first; date < last; date = date.AddDays(1.0))
                    {
                        if (predicate(date))
                        {
                            ++businessDays;
                        }
                    }

                    if (predicate(last))
                    {
                        ++businessDays;
                    }
                }
                else
                {
                    for (DateTime date = last; date < first; date = date.AddDays(1.0))
                    {
                        if (predicate(date))
                        {
                            ++businessDays;
                        }
                    }

                    if (predicate(first))
                    {
                        ++businessDays;
                    }
                }

                if (!includeFirst && predicate(first))
                {
                    --businessDays;
                }

                if (!includeLast && predicate(last))
                {
                    --businessDays;
                }

                if (first > last)
                {
                    businessDays = -businessDays;
                }
            }

            return businessDays;
        }
    }
}
