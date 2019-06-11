namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// Error messages used in parameter validation.
    /// </summary>
    internal static class ErrorMessages
    {
        /// <summary>
        /// The field is required.
        /// </summary>
        internal const string FieldIsRequired = "The field {0} is required.";

        /// <summary>
        /// The field must be positive.
        /// </summary>
        internal const string FieldMustBePositive = "The field {0} must be positive.";

        /// <summary>
        /// The field must be positive with a minimal value of 4.
        /// </summary>
        internal const string FieldMustBePositiveMinValue4 = "The field {0} must be positive with minimal value 4.";

        /// <summary>
        /// The field must be positive with a minimal value of 2.
        /// </summary>
        internal const string FieldMustBePositiveMinValue2 = "The field {0} must be positive with minimal value 2.";

        /// <summary>
        /// The field must not be negative.
        /// </summary>
        internal const string FieldMustNotBeNegative = "The field {0} must not be negative.";

        /// <summary>
        /// The field must be in range [0, 1].
        /// </summary>
        internal const string FieldMustBeInRange01 = "The field {0} must be in range [0, 1].";

        /// <summary>
        /// The field must be in range [-1, 1].
        /// </summary>
        internal const string FieldMustBeInRangeMin1Plus1 = "The field {0} must be in range [-1, 1].";

        /// <summary>
        /// The field value doesn't exist within the enum.
        /// </summary>
        internal const string FieldEnumValueInvalid = "The field {0} value doesn't exist within the enum.";

        /// <summary>
        /// The field TimeParameters must not be null.
        /// </summary>
        internal const string FieldTimeParametersMustNotBeNull = "The field TimeParameters must not be null.";

        /// <summary>
        /// The field WaveformParameters must not be null.
        /// </summary>
        internal const string FieldWaveformParametersMustNotBeNull = "The field WaveformParameters must not be null.";

        /// <summary>
        /// The field FbmParameters must not be null.
        /// </summary>
        internal const string FieldFbmParametersMustNotBeNull = "The field FbmParameters must not be null.";

        /// <summary>
        /// The field SquareParameters must not be null.
        /// </summary>
        internal const string FieldSquareParametersMustNotBeNull = "The field SquareParameters must not be null.";

        /// <summary>
        /// The field SawtoothParameters must not be null.
        /// </summary>
        internal const string FieldSawtoothParametersMustNotBeNull = "The field SawtoothParameters must not be null.";

        /// <summary>
        /// The field ChirpParameters must not be null.
        /// </summary>
        internal const string FieldChirpParametersMustNotBeNull = "The field ChirpParameters must not be null.";

        /// <summary>
        /// The field SinusoidalParameters must not be null.
        /// </summary>
        internal const string FieldSinusoidalParametersMustNotBeNull = "The field SinusoidalParameters must not be null.";

        /// <summary>
        /// The field MultiSinusoidalComponents must not be empty.
        /// </summary>
        internal const string FieldMultiSinusoidalComponentsMustNotBeEmpty = "The field MultiSinusoidalComponents must not be empty.";

        /// <summary>
        /// The field MultiSinusoidalParameters must not be null.
        /// </summary>
        internal const string FieldMultiSinusoidalParametersMustNotBeNull = "The field MultiSinusoidalParameters must not be null.";

        /// <summary>
        /// The field OhlcvParameters must not be null.
        /// </summary>
        internal const string FieldOhlcvParametersMustNotBeNull = "The field OhlcvParameters must not be null.";

        /// <summary>
        /// The field QuoteParameters must not be null.
        /// </summary>
        internal const string FieldQuoteParametersMustNotBeNull = "The field QuoteParameters must not be null.";

        /// <summary>
        /// The field TradeParameters must not be null.
        /// </summary>
        internal const string FieldTradeParametersMustNotBeNull = "The field TradeParameters must not be null.";

        /// <summary>
        /// The TimeParameters field name.
        /// </summary>
        internal const string TimeParameters = "TimeParameters";

        /// <summary>
        /// The WaveformParameters field name.
        /// </summary>
        internal const string WaveformParameters = "WaveformParameters";

        /// <summary>
        /// The FbmParameters field name.
        /// </summary>
        internal const string FbmParameters = "FbmParameters";

        /// <summary>
        /// The SquareParameters field name.
        /// </summary>
        internal const string SquareParameters = "SquareParameters";

        /// <summary>
        /// The SawtoothParameters field name.
        /// </summary>
        internal const string SawtoothParameters = "SawtoothParameters";

        /// <summary>
        /// The ChirpParameters field name.
        /// </summary>
        internal const string ChirpParameters = "ChirpParameters";

        /// <summary>
        /// The SinusoidalParameters field name.
        /// </summary>
        internal const string SinusoidalParameters = "SinusoidalParameters";

        /// <summary>
        /// The MultiSinusoidalComponents field name.
        /// </summary>
        internal const string MultiSinusoidalComponents = "MultiSinusoidalComponents";

        /// <summary>
        /// The MultiSinusoidalParameters field name.
        /// </summary>
        internal const string MultiSinusoidalParameters = "MultiSinusoidalParameters";

        /// <summary>
        /// The OhlcvParameters field name.
        /// </summary>
        internal const string OhlcvParameters = "OhlcvParameters";

        /// <summary>
        /// The QuoteParameters field name.
        /// </summary>
        internal const string QuoteParameters = "QuoteParameters";

        /// <summary>
        /// The TradeParameters field name.
        /// </summary>
        internal const string TradeParameters = "TradeParameters";

        /// <summary>
        /// The WaveformParameters.WaveformSamples field name.
        /// </summary>
        internal const string WaveformParametersWaveformSamples = "WaveformParameters.WaveformSamples";

        /// <summary>
        /// The field WaveformParameters.WaveformSamples must be the power of two if this FBM algorithm is used.
        /// </summary>
        internal const string WaveformParametersWaveformSamplesPowerOfTwo =
            "The field WaveformParameters.WaveformSamples must be the power of two if this FBM algorithm is used.";
    }
}