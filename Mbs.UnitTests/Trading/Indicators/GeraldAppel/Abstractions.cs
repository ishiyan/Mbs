using System;
using System.Collections.Generic;
using System.Text;
using Mbs.Trading.Data;
using Mbs.Trading.Indicators.Abstractions;

namespace Mbs.UnitTests.Trading.Indicators.GeraldAppel
{
    public enum IndicatorType
    {
        MovingAverageConvergenceDivergence = 0,
        SimpleMovingAverage = 1, // value
        ExponentialMovingAverage = 11, // value
        DoubleExponentialMovingAverage = 12, // value
        TripleExponentialMovingAverage = 13, // value
        T3ExponentialMovingAverage = 14, // value
        WeightedMovingAverage = 15, // value
        TriangularMovingAverage = 16, // value
        KaufmanAdaptiveMovingAverage = 17, // value
        WilliamsPercentR = 2,
        StochasticOscillator = 3,
        PercentagePriceOscillator = 31, // value, FastMovingAverageLine, SlowMovingAverageLine
        AbsolutePriceOscillator = 311, // value, FastMovingAverageLine, SlowMovingAverageLine
        RateOfChange = 32, // value
        RateOfChangePercentage = 33, // value
        Momentum = 34, // value
        OnBalanceVolume = 35, // value
        MoneyFlowIndex = 36, // value
        BalanceOfPower = 37, // value
        BollingerBands = 38, // upper, middle, lower lines; BandWidth line; PercentBand line; StandardDeviation line, source indicator line
        CommodityChannelIndex = 39, // value
        ChandeMomentumOscillator = 391, // value
        Variance = 4, // value
        StandardDeviation = 5 // value
    }
    public enum OutputType
    {
        Scalar = 0,
        Band = 1,
        HeatMap = 2
    }
    public enum OutputKind
    {
        SimpleMovingAverageScalar = IndicatorType.SimpleMovingAverage * 1000 + OutputType.Scalar * 100 + 0,
        ExponentialMovingAverageScalar = IndicatorType.ExponentialMovingAverage * 1000 + OutputType.Scalar * 100 + 0,
        DoubleExponentialMovingAverageScalar = IndicatorType.DoubleExponentialMovingAverage * 1000 + OutputType.Scalar * 100 + 0,
        TripleExponentialMovingAverageScalar = IndicatorType.TripleExponentialMovingAverage * 1000 + OutputType.Scalar * 100 + 0,
        T3ExponentialMovingAverageScalar = IndicatorType.T3ExponentialMovingAverage * 1000 + OutputType.Scalar * 100 + 0,
        WeightedMovingAverageScalar = IndicatorType.WeightedMovingAverage * 1000 + OutputType.Scalar * 100 + 0,
        TriangularMovingAverageScalar = IndicatorType.TriangularMovingAverage * 1000 + OutputType.Scalar * 100 + 0,
        KaufmanAdaptiveMovingAverageScalar = IndicatorType.KaufmanAdaptiveMovingAverage * 1000 + OutputType.Scalar * 100 + 0,
        WilliamsPercentRScalar = IndicatorType.WilliamsPercentR * 1000 + OutputType.Scalar * 100 + 0,
        StochasticOscillatorScalar = IndicatorType.StochasticOscillator * 1000 + OutputType.Scalar * 100 + 0,

        PercentagePriceOscillatorScalar = IndicatorType.PercentagePriceOscillator * 1000 + OutputType.Scalar * 100 + 0,
        PercentagePriceOscillatorFastMovingAverageScalar = IndicatorType.PercentagePriceOscillator * 1000 + OutputType.Scalar * 100 + 1,
        PercentagePriceOscillatorSlowMovingAverageScalar = IndicatorType.PercentagePriceOscillator * 1000 + OutputType.Scalar * 100 + 2,

        AbsolutePriceOscillatorScalar = IndicatorType.AbsolutePriceOscillator * 1000 + OutputType.Scalar * 100 + 0,
        AbsolutePriceOscillatorFastMovingAverageScalar = IndicatorType.AbsolutePriceOscillator * 1000 + OutputType.Scalar * 100 + 1,
        AbsolutePriceOscillatorSlowMovingAverageScalar = IndicatorType.AbsolutePriceOscillator * 1000 + OutputType.Scalar * 100 + 2,

        RateOfChangeScalar = IndicatorType.RateOfChange * 1000 + OutputType.Scalar * 100 + 0,
        RateOfChangePercentageScalar = IndicatorType.RateOfChangePercentage * 1000 + OutputType.Scalar * 100 + 0,
        MomentumScalar = IndicatorType.Momentum * 1000 + OutputType.Scalar * 100 + 0,
        ChandeMomentumOscillatorScalar = IndicatorType.ChandeMomentumOscillator * 1000 + OutputType.Scalar * 100 + 0,
        OnBalanceVolumeScalar = IndicatorType.OnBalanceVolume * 1000 + OutputType.Scalar * 100 + 0,
        MoneyFlowIndexScalar = IndicatorType.MoneyFlowIndex * 1000 + OutputType.Scalar * 100 + 0,
        BalanceOfPowerScalar = IndicatorType.BalanceOfPower * 1000 + OutputType.Scalar * 100 + 0,
        CommodityChannelIndexScalar = IndicatorType.CommodityChannelIndex * 1000 + OutputType.Scalar * 100 + 0,
        VarianceScalar = IndicatorType.Variance * 1000 + OutputType.Scalar * 100 + 0,
        StandardDeviationScalar = IndicatorType.StandardDeviation * 1000 + OutputType.Scalar * 100 + 0,

        // ReSharper disable InconsistentNaming
        MovingAverageConvergenceDivergence_Scalar = IndicatorType.MovingAverageConvergenceDivergence * 1000 + OutputType.Scalar * 100 + 0,
        MovingAverageConvergenceDivergence_SlowMovingAverage_Scalar = IndicatorType.MovingAverageConvergenceDivergence * 1000 + OutputType.Scalar * 100 + 1,
        MovingAverageConvergenceDivergence_FastMovingAverage_Scalar = IndicatorType.MovingAverageConvergenceDivergence * 1000 + OutputType.Scalar * 100 + 2,
        MovingAverageConvergenceDivergence_SignalMovingAverage_Scalar = IndicatorType.MovingAverageConvergenceDivergence * 1000 + OutputType.Scalar * 100 + 3,
        MovingAverageConvergenceDivergence_SignalMovingAverage_Band = IndicatorType.MovingAverageConvergenceDivergence * 1000 + OutputType.Band * 100 + 3,
        MovingAverageConvergenceDivergence_Histogram_Scalar = IndicatorType.MovingAverageConvergenceDivergence * 1000 + OutputType.Scalar * 100 + 4,
        MovingAverageConvergenceDivergence_Histogram_Band = IndicatorType.MovingAverageConvergenceDivergence * 1000 + OutputType.Band * 100 + 5,
    }
    public class Output1                     //  List<OutputKind>, IList<object> 
    {
        public OutputKind Kind;
        public object Data; // Scalar | Band | HeatMap   Array<Scalar>
    }
    public class Outputs1
    {
        public Output1[] Output;
    }
    public class Output2
    {
        public OutputKind Kind;       //  int Output Id = IndicatorKind * 100 + OutputKind
        public OutputType Type;
        public int Offset;
    }

    public class Outputs2           // OutputKind is global, contains all indicators
    {                               //  
        public Output2[] Index;
        public Scalar[] Scalars;
        public Band[] Bands;
        public HeatMap[] HeatMaps;
    }

    class Abstractions
    {
    }
}
