using System;
using Mbs.Numerics.RandomGenerators;
using Mbs.Numerics.RandomGenerators.FractionalBrownianMotion;
using Mbs.Trading.Data.Generators.Chirp;
using Mbs.Trading.Data.Generators.Sawtooth;
using Mbs.Trading.Time;
using Mbs.Trading.Time.Conventions;

namespace Mbs.Trading.Data.Generators
{
    /// <summary>
    /// Specifies the default values for data generators.
    /// </summary>
    internal static class DefaultParameterValues
    {
        /// <summary>
        /// The default value of the number of samples to generate.
        /// </summary>
        internal const int SamplesCount = 128;

        /// <summary>
        /// The default value of the time of the beginning of the trading session as string.
        /// </summary>
        internal const string SessionStartTimeString = "09:00:00";

        /// <summary>
        /// The default value of the end time of the trading session as string.
        /// </summary>
        internal const string SessionEndTimeString = "18:00:00";

        /// <summary>
        /// The default value of the date of the first data sample as string.
        /// </summary>
        internal const string StartDateString = "2000-01-03";

        /// <summary>
        /// The default value of the time granularity of data samples.
        /// </summary>
        internal const TimeGranularity TimeGranularity = Time.TimeGranularity.Day1;

        /// <summary>
        /// The default value of an exchange holiday schedule or a general country holiday schedule.
        /// </summary>
        internal const BusinessDayCalendar BusinessDayCalendar = Time.Conventions.BusinessDayCalendar.WeekendsOnly;

        /// <summary>
        /// The default value of the number of samples in the waveform.
        /// </summary>
        internal const int WaveformSamples = 128;

        /// <summary>
        /// The default value of the number of samples before the very first waveform sample.
        /// </summary>
        internal const int OffsetSamples = 0;

        /// <summary>
        /// The default value of the number of repetitions of the waveform. The value of zero means infinite.
        /// </summary>
        internal const int RepetitionsCount = 0;

        /// <summary>
        /// The default value of the amplitude of the noise as a fraction of the sample value.
        /// </summary>
        internal const double NoiseAmplitudeFraction = 0.01;

        /// <summary>
        /// The default value of the kind of an uniform random generator to produce the noise.
        /// </summary>
        internal const UniformRandomGeneratorKind NoiseUniformRandomGeneratorKind = UniformRandomGeneratorKind.Well44497A;

        /// <summary>
        /// The default value of the seed of a random generator to produce the noise.
        /// </summary>
        internal const int NoiseUniformRandomGeneratorSeed = 12345678;

        /// <summary>
        /// The default value of the shadow fraction, ρs, which determines the length of the candlestick shadows as a fraction of the mid price; ρs∈[0, 1]. The value should be greater or equal to the  <see cref="CandlestickBodyFraction"/>.
        /// </summary>
        internal const double CandlestickShadowFraction = 0.3;

        /// <summary>
        /// The default value of the body fraction, ρb, which determines the half-length of the candlestick body as a fraction of the mid price; ρb∈[0, 1]. The value should be less or equal to the <see cref="CandlestickShadowFraction"/>.
        /// </summary>
        internal const double CandlestickBodyFraction = 0.2;

        /// <summary>
        /// The default value of the value of the volume, which is the same for all candlesticks; should be positive.
        /// </summary>
        internal const double CandlestickVolume = 100;

        /// <summary>
        /// The default value of the spread fraction, ρs, which determines the ask and bid prices as a fraction of the mid price.
        /// </summary>
        internal const double SpreadFraction = 0.1;

        /// <summary>
        /// The default value of the ask size, which is the same for all quotes; should be positive.
        /// </summary>
        internal const double AskSize = 10;

        /// <summary>
        /// The default value of the bid size, which is the same for all quotes; should be positive.
        /// </summary>
        internal const double BidSize = 10;

        /// <summary>
        /// The default value of the volume, which is the same for all trades; should be positive.
        /// </summary>
        internal const double TradeVolume = 100;

        /// <summary>
        /// The default value of the amplitude of the fractional Brownian motion, should be positive.
        /// </summary>
        internal const double FbmAmplitude = 100;

        /// <summary>
        /// The default value of the minimum of the fractional Brownian motion, should be positive.
        /// </summary>
        internal const double FbmMinimalValue = 10;

        /// <summary>
        /// The default value of the Hurst exponent of the fractal Brownian motion, H∈[0, 1].
        /// </summary>
        internal const double FbmHurstExponent = 0.63;

        /// <summary>
        /// The default value of the fractional Brownian motion algorithm.
        /// </summary>
        internal const FractionalBrownianMotionAlgorithm FbmAlgorithm = FractionalBrownianMotionAlgorithm.Hosking;

        /// <summary>
        /// The default value of the kind of a Gaussian distribution random generator.
        /// </summary>
        internal const NormalRandomGeneratorKind FbmNormalRandomGeneratorKind = NormalRandomGeneratorKind.ZigguratColinGreen;

        /// <summary>
        /// The default value of the kind of a uniform random generator optionally associated with the Gaussian random generator.
        /// </summary>
        internal const UniformRandomGeneratorKind FbmAssociatedUniformRandomGeneratorKind = UniformRandomGeneratorKind.Well44497A;

        /// <summary>
        /// The default value of the seed of a random generator.
        /// </summary>
        internal const int FbmSeed = 123456789;

        /// <summary>
        /// The default value of the amplitude of the geometric Brownian motion, should be positive.
        /// </summary>
        internal const double GbmAmplitude = 100;

        /// <summary>
        /// The default value of the minimum of the geometric Brownian motion, should be positive.
        /// </summary>
        internal const double GbmMinimalValue = 10;

        /// <summary>
        /// The default value of the drift, μ, of the geometric Brownian motion.
        /// </summary>
        internal const double GbmDrift = 0.003;

        /// <summary>
        /// The default value of the volatility, σ, of the geometric Brownian motion.
        /// </summary>
        internal const double GbmVolatility = 0.3;

        /// <summary>
        /// The default value of the kind of a Gaussian distribution random generator.
        /// </summary>
        internal const NormalRandomGeneratorKind GbmNormalRandomGeneratorKind = NormalRandomGeneratorKind.ZigguratColinGreen;

        /// <summary>
        /// The default value of the kind of a uniform random generator optionally associated with the Gaussian random generator.
        /// </summary>
        internal const UniformRandomGeneratorKind GbmAssociatedUniformRandomGeneratorKind = UniformRandomGeneratorKind.Well44497A;

        /// <summary>
        /// The default value of the seed of a random generator.
        /// </summary>
        internal const int GbmSeed = 123456789;

        /// <summary>
        /// The default value of the amplitude of the square impulse, should be positive.
        /// </summary>
        internal const double SquareAmplitude = 100;

        /// <summary>
        /// The default value of the minimum of the square impulse, should be positive.
        /// </summary>
        internal const double SquareMinimalValue = 10;

        /// <summary>
        /// The default value of the amplitude of the sawtooth impulse, should be positive.
        /// </summary>
        internal const double SawtoothAmplitude = 100;

        /// <summary>
        /// The default value of the minimum of the sawtooth impulse, should be positive.
        /// </summary>
        internal const double SawtoothMinimalValue = 10;

        /// <summary>
        /// The default value indicating whether the sawtooth is reflected horizontally to form a triangle shape.
        /// </summary>
        internal const bool SawtoothIsBiDirectional = false;

        /// <summary>
        /// The default value of the sawtooth shape.
        /// </summary>
        internal const SawtoothShape SawtoothShape = Sawtooth.SawtoothShape.Linear;

        /// <summary>
        /// The default value of the amplitude of the chirp, should be positive.
        /// </summary>
        internal const double ChirpAmplitude = 100;

        /// <summary>
        /// The default value of the minimum of the chirp, should be positive.
        /// </summary>
        internal const double ChirpMinimalValue = 10;

        /// <summary>
        /// The default value of the instantaneous initial period of the chirp in samples, should be ≥ 2.
        /// </summary>
        internal const double ChirpInitialPeriod = 128;

        /// <summary>
        /// The default value of the instantaneous final period of the chirp in samples, should be ≥ 2.
        /// </summary>
        internal const double ChirpFinalPeriod = 16;

        /// <summary>
        /// The default value of the initial phase, φ, of the chirp in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        internal const double ChirpPhaseInPi = 0;

        /// <summary>
        /// The default value indicating whether the period of even chirps descends from the final period to the initial one, to form a symmetrical shape with odd chirps.
        /// </summary>
        internal const bool ChirpIsBiDirectional = false;

        /// <summary>
        /// The default value of the chirp sweep.
        /// </summary>
        internal const ChirpSweep ChirpSweep = Chirp.ChirpSweep.LinearPeriod;

        /// <summary>
        /// The default value of the amplitude of the sinusoid, should be positive.
        /// </summary>
        internal const double SinusoidalAmplitude = 100;

        /// <summary>
        /// The default value of the minimum of the sinusoid, should be positive.
        /// </summary>
        internal const double SinusoidalMinimalValue = 10;

        /// <summary>
        /// The default value of the period of the sinusoid in samples, should be ≥ 2.
        /// </summary>
        internal const double SinusoidalPeriod = 16;

        /// <summary>
        /// The default value of the phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        internal const double SinusoidalPhaseInPi = 0;

        /// <summary>
        /// The default value of the amplitude of the sinusoidal component, should be positive.
        /// </summary>
        internal const double MultiSinusoidalAmplitude = 10;

        /// <summary>
        /// The default value of the minimum of the aggregated multi sinusoid waveform, should be positive.
        /// </summary>
        internal const double MultiSinusoidalMinimalValue = 10;

        /// <summary>
        /// The default value of the period of the sinusoidal component in samples, should be ≥ 2.
        /// </summary>
        internal const double MultiSinusoidalPeriod = 16;

        /// <summary>
        /// The default value of the phase, φ, of the sinusoidal component in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        internal const double MultiSinusoidalPhaseInPi = 0;

        /// <summary>
        /// The default value of the time of the beginning of the trading session.
        /// </summary>
        internal static readonly TimeSpan SessionStartTime = new TimeSpan(9, 0, 0);

        /// <summary>
        /// The default value of the end time of the trading session.
        /// </summary>
        internal static readonly TimeSpan SessionEndTime = new TimeSpan(18, 0, 0);

        /// <summary>
        /// The default value of the date of the first data sample.
        /// </summary>
        internal static readonly DateTime StartDate = new DateTime(2000, 1, 3);
    }
}