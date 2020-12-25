namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// Identifies an indicator.
    /// </summary>
    public enum IndicatorType
    {
 /*
        /// <summary>
        /// Moving Average Convergence Divergence (MACD).
        /// </summary>
        MovingAverageConvergenceDivergence,
*/

        /// <summary>
        /// Simple Moving Average (SMA).
        /// </summary>
        SimpleMovingAverage,

        /// <summary>
        /// Exponential Moving Average (EMA).
        /// </summary>
        ExponentialMovingAverage,
/*
        /// <summary>
        /// Double Exponential Moving Average (DEMA).
        /// </summary>
        DoubleExponentialMovingAverage,

        /// <summary>
        /// Triple Exponential Moving Average (TEMA).
        /// </summary>
        TripleExponentialMovingAverage,

        /// <summary>
        /// T3 Exponential Moving Average (T3EMA).
        /// </summary>
        T3ExponentialMovingAverage,

        /// <summary>
        /// Weighted Moving Average (WMA).
        /// </summary>
        WeightedMovingAverage,

        /// <summary>
        /// Triangular Moving Average (TRIMA).
        /// </summary>
        TriangularMovingAverage,

        /// <summary>
        /// Kaufman Adaptive Moving Average (KAMA).
        /// </summary>
        KaufmanAdaptiveMovingAverage,

        /// <summary>
        /// Williams %R (WILL%R).
        /// </summary>
        WilliamsPercentR,

        /// <summary>
        /// Stochastic Oscillator (STO).
        /// </summary>
        StochasticOscillator,

        /// <summary>
        /// Percentage Price Oscillator (PPO).
        /// </summary>
        PercentagePriceOscillator,

        /// <summary>
        /// Absolute Price Oscillator (APO).
        /// </summary>
        AbsolutePriceOscillator,

        /// <summary>
        /// Rate of Change (RoC).
        /// </summary>
        RateOfChange,

        /// <summary>
        /// Rate of Change % (RoC%).
        /// </summary>
        RateOfChangePercentage,

        /// <summary>
        /// Momentum (MOM).
        /// </summary>
        Momentum,

        /// <summary>
        /// On-Balance Volume (OBV).
        /// </summary>
        OnBalanceVolume,

        /// <summary>
        /// Money Flow Index (MFI).
        /// </summary>
        MoneyFlowIndex,

        /// <summary>
        /// Balance of Power (BoP).
        /// </summary>
        BalanceOfPower,
*/

        /// <summary>
        /// Bollinger Bands (BB).
        /// </summary>
        BollingerBands,
/*
        /// <summary>
        /// Commodity Channel Index (CCI).
        /// </summary>
        CommodityChannelIndex,

        /// <summary>
        /// Chande Momentum Oscillator (CMO).
        /// </summary>
        ChandeMomentumOscillator,
*/

        /// <summary>
        /// Variance (VAR).
        /// </summary>
        Variance,

        /// <summary>
        /// Standard Deviation (STDEV).
        /// </summary>
        StandardDeviation,

        /// <summary>
        /// Goertzel power spectrum (GOERTZEL).
        /// </summary>
        GoertzelSpectrum,

        /// <summary>
        /// Unknown indicator.
        /// </summary>
        Unknown,
    }
}
