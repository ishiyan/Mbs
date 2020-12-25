namespace Mbs.Trading.Time.Conventions
{
    /// <summary>
    /// Provides a specific exchange holiday schedule or a general country holiday schedule.
    /// </summary>
    public enum BusinessDayCalendar
    {
        /// <summary>
        /// TARGET (Trans-european Automated Real-time Gross settlement Express Transfer system)
        /// <para/>
        /// holiday schedule, see <c>http://www.ecb.int</c>. The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Good Friday, Easter Monday, Labour Day, Christmas Day, Boxing Day.
        /// </summary>
        Target,

        /// <summary>
        /// Weekends-only holiday schedule.
        /// </summary>
        WeekendsOnly,

        /// <summary>
        /// A no-holidays (physical days) schedule.
        /// </summary>
        NoHolidays,

        /// <summary>
        /// The Euronext exchange holiday schedule. The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Good Friday, Easter Monday, Labour Day, Christmas Day, Boxing Day.
        /// </summary>
        Euronext,

        /// <summary>
        /// The generic US exchange holiday schedule. The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Martin Luther King's birthday, Presidents' Day, Good Friday, Memorial Day, Independence Day, Labour Day, Thanksgiving Day, Christmas Day.
        /// <para/>
        /// Columbus Day and Veterans' Day are excluded.
        /// </summary>
        UnitedStates,

        /// <summary>
        /// The generic Swiss exchange holiday schedule. The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Berchtoldstag, Good Friday, Easter Monday, Ascension Day, Whit (Pentecost) Monday, Labour Day, National Day, Christmas Eve, Christmas Day, St. Stephen's Day, New Year's Eve.
        /// </summary>
        Switzerland,

        /// <summary>
        /// The generic Swedish exchange holiday schedule. The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Epiphany Day, Good Friday, Easter Monday, General Prayer Day, Ascension Day, Whit (Pentecost) Monday, Labour Day, National Day, Midsummer Eve, Christmas Eve, Christmas Day, Boxing Day, New Year's Eve.
        /// </summary>
        Sweden,

        /// <summary>
        /// The generic Danish exchange holiday schedule. The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Maunday Thursday, Good Friday, Easter Monday, General Prayer Day, Ascension Day, Whit (Pentecost) Monday, Constitution Day, Christmas Eve, Christmas Day, Boxing Day, New Year's Eve.
        /// </summary>
        Denmark,

        /// <summary>
        /// The generic Norwegian exchange holiday schedule. The holidays (apart from weekends) are:
        /// <para/>
        /// New Year's Day, Maunday Thursday, Good Friday, Easter Monday, Labour Day, Constitution Day, Ascension Day, Whit (Pentecost) Monday, Labour Day, National Day, Midsummer Eve, Christmas Eve, Christmas Day, Boxing Day, New Year's Eve.
        /// </summary>
        Norway,
    }
}
