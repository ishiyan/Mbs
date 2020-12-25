using System;

namespace Mbs.Trading.Time.Conventions
{
    /// <summary>
    /// Day count convention extensions.
    /// </summary>
    public static class DayCountConventionExtensions
    {
        /// <summary>
        /// A time span between the start date and the end date using the specified day count convention and expressed in whole days.
        /// </summary>
        /// <param name="start">The start date and time.</param>
        /// <param name="end">The end date and time.</param>
        /// <param name="dayCountConvention">The day count convention.</param>
        /// <returns>A time span between the start date and the end date, expressed in whole days.</returns>
        public static int DayCountTo(this DateTime start, DateTime end, DayCountConvention dayCountConvention)
        {
            if (start > end)
            {
                DateTime temp = start;
                start = end;
                end = temp;
            }

            return dayCountConvention switch
            {
                DayCountConvention.Thirty360American => start.Thirty360DayCount(end, dayCountConvention),
                DayCountConvention.Thirty360European => start.Thirty360DayCount(end, dayCountConvention),
                DayCountConvention.Thirty360German => start.Thirty360DayCount(end, dayCountConvention),
                _ => (int)(end - start).TotalDays
            };
        }

        /// <summary>
        /// A time span between the start date and the end date using the specified day count convention and expressed in a fraction of a day.
        /// </summary>
        /// <param name="start">The start date and time.</param>
        /// <param name="end">The end date and time.</param>
        /// <param name="dayCountConvention">The day count convention.</param>
        /// <returns>A time span between the start date and the end date, expressed as a fraction of a day.</returns>
        public static double DayFractionTo(this DateTime start, DateTime end, DayCountConvention dayCountConvention)
        {
            if (start > end)
            {
                DateTime temp = start;
                start = end;
                end = temp;
            }

            switch (dayCountConvention)
            {
                case DayCountConvention.Thirty360American:
                case DayCountConvention.Thirty360European:
                case DayCountConvention.Thirty360German:
                    int days = start.Thirty360DayCount(end, dayCountConvention);
                    return days - start.TimeOfDay.TotalDays + end.TimeOfDay.TotalDays;
                default:
                    return (end - start).TotalDays;
            }
        }

        /// <summary>
        /// A time span between the start date and the end date using the specified day count convention and expressed as a fraction of a year.
        /// </summary>
        /// <param name="start">The start date and time.</param>
        /// <param name="end">The end date and time.</param>
        /// <param name="dayCountConvention">The day count convention.</param>
        /// <param name="dropTimeOfDay">Whether to drop time of day and consider only whole days.</param>
        /// <returns>A time span between the start date and the end date, expressed as a fraction of a year.</returns>
        public static double YearFractionTo(this DateTime start, DateTime end, DayCountConvention dayCountConvention, bool dropTimeOfDay = false)
        {
            if (start > end)
            {
                DateTime temp = start;
                start = end;
                end = temp;
            }

            switch (dayCountConvention)
            {
                case DayCountConvention.ActualActualIsda:
                    return YearFractionActualActualHistorical(start, end, dropTimeOfDay);
                case DayCountConvention.ActualActualAfb:
                    return YearFractionActualActualEuro(start, end, dropTimeOfDay);
                case DayCountConvention.ActualActualIsma:
                    return YearFractionActualActualBond(start, end, start, end, dropTimeOfDay);
            }

            if (start == end)
            {
                return 0;
            }

            double days = dropTimeOfDay ? start.DayCountTo(end, dayCountConvention) : start.DayFractionTo(end, dayCountConvention);
            switch (dayCountConvention)
            {
                case DayCountConvention.Thirty360American:
                case DayCountConvention.Thirty360European:
                case DayCountConvention.Thirty360German:
                case DayCountConvention.Actual360:
                    return days / 360;
                default:
                    return days / 365;
            }
        }

        /// <summary>
        /// A time span between this date and the end date using the specified day count convention and expressed as a fraction of a year.
        /// </summary>
        /// <param name="start">This date and time.</param>
        /// <param name="end">The end date and time.</param>
        /// <param name="referenceStart">The reference start date and time.</param>
        /// <param name="referenceEnd">The reference end date and time.</param>
        /// <param name="dayCountConvention">The day count convention.</param>
        /// <param name="dropTimeOfDay">Whether to drop time of day and consider only whole days.</param>
        /// <returns>A time span between this date and the end date, expressed as a fraction of a year.</returns>
        public static double YearFractionTo(this DateTime start, DateTime end, DateTime referenceStart, DateTime referenceEnd, DayCountConvention dayCountConvention, bool dropTimeOfDay = false)
        {
            if (start > end)
            {
                DateTime temp = start;
                start = end;
                end = temp;
            }

            switch (dayCountConvention)
            {
                case DayCountConvention.ActualActualIsda:
                    return YearFractionActualActualHistorical(start, end, dropTimeOfDay);
                case DayCountConvention.ActualActualAfb:
                    return YearFractionActualActualEuro(start, end, dropTimeOfDay);
                case DayCountConvention.ActualActualIsma:
                    return YearFractionActualActualBond(start, end, referenceStart, referenceEnd, dropTimeOfDay);
            }

            if (start == end)
            {
                return 0;
            }

            double days = dropTimeOfDay ? start.DayCountTo(end, dayCountConvention) : start.DayFractionTo(end, dayCountConvention);
            switch (dayCountConvention)
            {
                case DayCountConvention.Thirty360American:
                case DayCountConvention.Thirty360European:
                case DayCountConvention.Thirty360German:
                case DayCountConvention.Actual360:
                    return days / 360;
                default:
                    return days / 365;
            }
        }

        private static double YearFractionActualActualHistorical(DateTime start, DateTime end, bool dropTimeOfDay)
        {
            if (start == end)
            {
                return 0;
            }

            int y1 = start.Year, y2 = end.Year;
            double days1 = DateTime.IsLeapYear(y1) ? 366 : 365;
            double days2 = DateTime.IsLeapYear(y2) ? 366 : 365;
            double sum = y2 - y1 - 1;
            if (dropTimeOfDay)
            {
                sum += (int)(new DateTime(y1 + 1, 1, 1) - start).TotalDays / days1;
                sum += (int)(end - new DateTime(y2, 1, 1)).TotalDays / days2;
                return sum;
            }

            sum += (new DateTime(y1 + 1, 1, 1) - start).TotalDays / days1;
            sum += (end - new DateTime(y2, 1, 1)).TotalDays / days2;
            return sum;
        }

        private static double YearFractionActualActualEuro(DateTime start, DateTime end, bool dropTimeOfDay)
        {
            if (start == end)
            {
                return 0;
            }

            DateTime newEnd = end, temp = end;
            double sum = 0;
            while (temp > start)
            {
                temp = newEnd.AddYears(-1);
                if (temp.Day == 28 && temp.Month == 2 && DateTime.IsLeapYear(temp.Year))
                {
                    temp = temp.AddDays(1);
                }

                if (temp < start)
                {
                    continue;
                }

                ++sum;
                newEnd = temp;
            }

            double den = 365;
            if (DateTime.IsLeapYear(newEnd.Year))
            {
                temp = new DateTime(newEnd.Year, 2, 29);
                if (newEnd > temp && start <= temp)
                {
                    ++den;
                }
            }
            else if (DateTime.IsLeapYear(start.Year))
            {
                temp = new DateTime(start.Year, 2, 29);
                if (newEnd > temp && start <= temp)
                {
                    ++den;
                }
            }

            den = (newEnd - start).TotalDays / den;
            return dropTimeOfDay ? sum + (int)den : sum + den;
        }

        private static double YearFractionActualActualBond(DateTime start, DateTime end, DateTime refPeriodStart, DateTime refPeriodEnd, bool dropTimeOfDay)
        {
            if (start == end)
            {
                return 0;
            }

            // Estimate roughly the length in months of a period.
            int months = (int)(0.5 + 12 * (refPeriodEnd - refPeriodStart).TotalDays / 365);
            if (months == 0)
            {
                // For short periods, take the reference period as 1 year from start.
                refPeriodStart = start;
                refPeriodEnd = start.AddYears(1);
                months = 12;
            }

            double period = months / 12d;
            if (end <= refPeriodEnd)
            {
                // Here refPeriodEnd is a future (notional?) payment date.
                if (start >= refPeriodStart)
                {
                    // Here refPeriodStart is the last (maybe notional) payment date.
                    double diff = (end - start).TotalDays;
                    double refDiff = (refPeriodEnd - refPeriodStart).TotalDays;
                    return dropTimeOfDay ? period * (int)diff / (int)refDiff : period * diff / refDiff;
                }

                // Here refPeriodStart is the next (maybe notional) payment date and refPeriodEnd is the second next (maybe notional) payment date.
                // start < refPeriodStart < refPeriodEnd AND end <= refPeriodEnd
                // This case is long first coupon.

                // The last notional payment date.
                DateTime previousRef = refPeriodStart.AddMonths(-months);
                if (end > refPeriodStart)
                {
                    return YearFractionActualActualBond(start, refPeriodStart, previousRef, refPeriodStart, dropTimeOfDay) +
                           YearFractionActualActualBond(refPeriodStart, end, refPeriodStart, refPeriodEnd, dropTimeOfDay);
                }

                return YearFractionActualActualBond(start, end, previousRef, refPeriodStart, dropTimeOfDay);
            }

            // Here refPeriodEnd is the last (notional?) payment date.
            // start < refPeriodEnd < end AND refPeriodStart < refPeriodEnd
            if (refPeriodStart > start)
            {
                throw new ArgumentException("Invalid dates: start < refPeriodStart < refPeriodEnd < end.");
            }

            // Now it is: refPeriodStart <= start < refPeriodEnd < end
            // The part from start to refPeriodEnd.
            double sum = YearFractionActualActualBond(start, refPeriodEnd, refPeriodStart, refPeriodEnd, dropTimeOfDay);

            // The part from refPeriodEnd to end.
            // Count how many regular periods are in [refPeriodEnd, end], then add the remaining time.
            int i = 0;
            DateTime newRefStart, newRefEnd;
            while (true)
            {
                newRefStart = refPeriodEnd.AddMonths(months * i);
                newRefEnd = refPeriodEnd.AddMonths(months * (i + 1));
                if (end < newRefEnd)
                {
                    break;
                }

                sum += period;
                ++i;
            }

            sum += YearFractionActualActualBond(newRefStart, end, newRefStart, newRefEnd, dropTimeOfDay);
            return sum;
        }

        private static int Thirty360DayCount(this DateTime start, DateTime end, DayCountConvention dayCountConvention)
        {
            int d1 = start.Day, m1 = start.Month, y1 = start.Year;
            int d2 = end.Day, m2 = end.Month, y2 = end.Year;
            switch (dayCountConvention)
            {
                case DayCountConvention.Thirty360European:
                {
                    if (d1 == 31)
                    {
                        d1 = 30;
                    }

                    if (d2 == 31)
                    {
                        d2 = 30;
                    }

                    break;
                }

                case DayCountConvention.Thirty360American:
                {
                    if (m1 == 2 && (DateTime.IsLeapYear(y1) ? 29 : 28) == d1)
                    {
                        d1 = 30;
                        if (m2 == 2 && (DateTime.IsLeapYear(y2) ? 29 : 28) == d2)
                        {
                            d2 = 30;
                        }
                    }

                    if (d1 == 31)
                    {
                        d1 = 30;
                    }

                    if (d1 == 30 && d2 == 31)
                    {
                        d2 = 30;
                    }

                    break;
                }

                default:
                {
                    // dayCountConvention == DayCountConvention.Thirty360German
                    if (m1 == 2 && (DateTime.IsLeapYear(y1) ? 29 : 28) == d1)
                    {
                        d1 = 30;
                    }

                    if (m2 == 2 && (DateTime.IsLeapYear(y2) ? 29 : 28) == d2)
                    {
                        d2 = 30;
                    }

                    if (d1 == 31)
                    {
                        d1 = 30;
                    }

                    if (d2 == 31)
                    {
                        d2 = 30;
                    }

                    break;
                }
            }

            return 360 * (y2 - y1) + 30 * (m2 - m1) + d2 - d1;
        }
    }
}
