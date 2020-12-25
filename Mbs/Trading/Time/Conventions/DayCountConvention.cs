namespace Mbs.Trading.Time.Conventions
{
    /// <summary>
    /// Available day count conventions.
    /// </summary>
    /// <remarks>
    /// See http://en.wikipedia.org/wiki/Day_count_convention.
    /// </remarks>
    public enum DayCountConvention
    {
        /// <summary>
        /// The 30/360 US (NASD) convention:
        /// <para>- if D2 is the last day of February (28 in a non leap year, 29 in a leap year) and D1 is the last day of February, change D2 to 30;</para>
        /// <para>- if D1 is the last day of February, change D1 to 30;</para>
        /// <para>- if D2 is 31 and D1 is 30 or 31, change D2 to 30;</para>
        /// <para>- if D1 is 31, change D1 to 30.</para>
        /// <para>N = (D2–D1) + 30(M2–M1) + 360(Y2–Y1)</para>
        /// <para>Known as "30U/360", "360/360 US", or "Bond Basis".</para>
        /// </summary>
        Thirty360American,

        /// <summary>
        /// The 30/360 German convention:
        /// <para>- the last day of February is treated as the 30th day of the month;</para>
        /// <para>- if D1 or D2 is 31, then use the value 30 instead.</para>
        /// <para>N = (D2–D1) + 30(M2–M1) + 360(Y2–Y1)</para>
        /// <para>Also known as "30E/360 ISDA", "Eurobond Basis (ISDA 2000)", "German".</para>
        /// </summary>
        Thirty360German,

        /// <summary>
        /// The 30/360 European convention:
        /// <para>- if D1 or D2 is 31, then use the value 30 instead;</para>
        /// <para>- the last day of February is not treated specially.</para>
        /// <para>N = (D2–D1) + 30(M2–M1) + 360(Y2–Y1)</para>
        /// <para>Also known as "30E/360", "30S/360", "30/360 ICMA", "Eurobond Basis (ISDA 2006)", "Special German".</para>
        /// </summary>
        Thirty360European,

        /// <summary>
        /// The Actual/Actual ISMA convention:
        /// <para>N = number of days between D1.M1.Y1 and D2.M2.Y2</para>
        /// <para>Also known as "ISMA and US Treasury convention", "Actual/Actual (Bond)", "Act/Act ISMA", "ISMA-99".</para>
        /// </summary>
        ActualActualIsma,

        /// <summary>
        /// The Actual/Actual AFB convention:
        /// <para>N = number of days between D1.M1.Y1 and D2.M2.Y2</para>
        /// <para>Also known as "AFB convention", "Actual/Actual (Euro)", "Actual/Actual AFB/FBF Master Agreement".</para>
        /// </summary>
        ActualActualAfb,

        /// <summary>
        /// The Actual/Actual ISDA convention:
        /// <para>N = number of days between D1.M1.Y1 and D2.M2.Y2</para>
        /// <para>Also known as "ISDA convention", "Actual/Actual (Historical)", "Actual/Actual", "Act/Act", "Actual/365", "Act/365", "A/365".</para>
        /// </summary>
        ActualActualIsda,

        /// <summary>
        /// The Actual/365 Fixed convention:
        /// <para>- if D1 or D2 is 31, then use the value 30 instead;</para>
        /// <para>- the last day of February is not treated specially.</para>
        /// <para>N = number of days between D1.M1.Y1 and D2.M2.Y2</para>
        /// <para>Also known as "Actual/365 Fixed", "Act/365 Fixed", "A/365 Fixed", "A/365F", "English".</para>
        /// </summary>
        Actual365Fixed,

        /// <summary>
        /// The Actual/360 convention:
        /// <para>N = number of days between D1.M1.Y1 and D2.M2.Y2</para>
        /// <para>Also known as "Actual/360", "Act/360", "A/360", "French".</para>
        /// </summary>
        Actual360,

        /// <summary>
        /// The Actual/365L convention:
        /// <para>N = number of days between D1.M1.Y1 and D2.M2.Y2</para>
        /// <para>Also known as "Actual/365L", "ISMA-Year".</para>
        /// </summary>
        Actual365L,

        /// <summary>
        /// A simple day counter for reproducing theoretical calculations.
        /// <para/>Ensures that whole-month distances are returned as a simple fraction, i.e., 1 year = 1.0, 6 months = 0.5, 3 months = 0.25 and so forth.
        /// <para/>This day counter should be used together with the null calendar, which ensures that dates at whole-month distances share the same day of month.
        /// </summary>
        Theoretical,
    }
}
