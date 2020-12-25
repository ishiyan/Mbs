namespace Mbs.Trading.Time.Conventions
{
    /// <summary>
    /// A convention of adjusting payment dates in response to days that are not TARGET (Trans-European Automated Real-Time Gross settlement Express Transfer system) business days.
    /// </summary>
    public enum BusinessDayConvention
    {
        /// <summary>
        /// Following (ISDA): choose the first business day after the given holiday.
        /// </summary>
        Following,

        /// <summary>
        /// Modified Following (ISDA): choose the first business day after the given holiday unless it belongs to a different month, in which case choose the first business day before the holiday.
        /// </summary>
        ModifiedFollowing,

        /// <summary>
        /// Preceding (ISDA): choose the first business day before the given holiday.
        /// </summary>
        Preceding,

        /// <summary>
        /// Modified Preceding (non-ISDA): choose the first business day before the given holiday unless it belongs to a different month, in which case choose the first business day after the holiday.
        /// </summary>
        ModifiedPreceding,

        /// <summary>
        /// Unadjusted: do not adjust, business holidays are effectively ignored.
        /// </summary>
        Unadjusted,
    }
}
