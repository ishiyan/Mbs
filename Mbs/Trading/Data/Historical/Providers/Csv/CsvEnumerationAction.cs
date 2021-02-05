namespace Mbs.Trading.Data.Historical.Providers.Csv
{
    /// <summary>
    /// Actions used to select a date range while enumerating entities.
    /// </summary>
    internal enum CsvEnumerationAction
    {
        /// <summary>
        /// Process current entity and continue enumeration.
        /// </summary>
        Continue,

        /// <summary>
        /// Do not process current entity and continue enumeration.
        /// </summary>
        Skip,

        /// <summary>
        /// Do not process current entity and skip enumeration.
        /// </summary>
        Break,
    }
}
