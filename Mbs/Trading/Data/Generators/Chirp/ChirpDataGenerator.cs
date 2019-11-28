using System;
using System.Globalization;
using Mbs.Numerics;
using Mbs.Numerics.Random;
using Mbs.Trading.Time;

namespace Mbs.Trading.Data.Generators.Chirp
{
    /// <summary>
    /// The chirp waveform (a period-swept cosine wave) generator produces samples which form
    /// a period-swept cosine wave defined by the given begin/end periods, amplitude and phase.
    /// An optional noise may be added to the samples.
    /// </summary>
    /// <typeparam name="T">A temporal entity type.</typeparam>
    public abstract class ChirpDataGenerator<T> : WaveformDataGenerator<T>
        where T : TemporalEntity, new()
    {
        /// <summary>
        /// Gets the amplitude of the chirp in sample units, should be positive.
        /// </summary>
        public double SampleAmplitude { get; }

        /// <summary>
        /// Gets the sample value corresponding to the minimum of the chirp, should be positive.
        /// </summary>
        public double SampleMinimum { get; }

        /// <summary>
        /// Gets the instantaneous initial period of the chirp in samples, should be ≥ 2.
        /// </summary>
        public double InitialPeriod { get; }

        /// <summary>
        /// Gets the instantaneous final period of the chirp in samples, should be ≥ 2.
        /// </summary>
        public double FinalPeriod { get; }

        /// <summary>
        /// Gets the initial phase, φ, of the chirp in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </summary>
        public double PhaseInPi { get; }

        /// <summary>
        /// Gets the chirp sweep.
        /// </summary>
        public ChirpSweep ChirpSweep { get; }

        /// <summary>
        /// Gets a value indicating whether the period of even chirps descends from the final period to the initial one, to form a symmetrical shape with odd chirps.
        /// </summary>
        public bool IsBiDirectional { get; }

        private readonly double lengthMinusOne;
        private readonly double initialFrequency;
        private readonly double finalFrequency;
        private readonly double summand;
        private readonly double phase;
        private readonly double ratio;
        private double angle;
        private double instantPeriod;
        private double instantFrequency;
        private bool directionUp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChirpDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="timeParameters">The time-related input parameters for the <see cref="TemporalDataGenerator{T}"/>.</param>
        /// <param name="waveformParameters">The input parameters for the <see cref="WaveformDataGenerator{T}"/>.</param>
        /// <param name="chirpParameters">The input parameters for the <see cref="ChirpDataGenerator{T}"/>.</param>
        protected ChirpDataGenerator(
            TimeParameters timeParameters,
            WaveformParameters waveformParameters,
            ChirpParameters chirpParameters)
            : base(timeParameters, waveformParameters)
        {
            SampleAmplitude = chirpParameters.Amplitude;
            SampleMinimum = chirpParameters.MinimalValue;
            summand = chirpParameters.MinimalValue + chirpParameters.Amplitude;
            InitialPeriod = chirpParameters.InitialPeriod;
            instantPeriod = chirpParameters.InitialPeriod;
            initialFrequency = Constants.TwoPi / chirpParameters.InitialPeriod;
            instantFrequency = initialFrequency;
            FinalPeriod = chirpParameters.FinalPeriod;
            finalFrequency = Constants.TwoPi / chirpParameters.FinalPeriod;
            ChirpSweep = chirpParameters.ChirpSweep;
            IsBiDirectional = chirpParameters.IsBiDirectional;
            PhaseInPi = chirpParameters.PhaseInPi;
            phase = Constants.Pi * chirpParameters.PhaseInPi;
            angle = phase;
            lengthMinusOne = WaveformSamples - 1;
            ratio = CalculateRatio(chirpParameters.ChirpSweep);
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChirpDataGenerator{T}"/> class.
        /// </summary>
        /// <param name="sessionBeginTime">The time of the beginning of the trading session.</param>
        /// <param name="sessionEndTime">The end time of the trading session.</param>
        /// <param name="startDate">The date of the first data sample.</param>
        /// <param name="timeGranularity">The time granularity of data samples.</param>
        /// <param name="businessDayCalendar">The value specifying an exchange holiday schedule or a general country holiday schedule.</param>
        /// <param name="waveformSamples">The number of samples in the waveform, should be ≥ 2..</param>
        /// <param name="offsetSamples">The number of samples before the very first waveform. The value of zero means the waveform starts immediately.</param>
        /// <param name="repetitionsCount">The number of repetitions of the waveform. The value of zero means infinite.</param>
        /// <param name="noiseAmplitudeFraction">The amplitude of the noise as a fraction of the mid price.</param>
        /// <param name="noiseRandomGenerator">The uniformly distributed random generator to produce the noise.</param>
        /// <param name="sampleAmplitude">The amplitude of the chirp impulse in sample units, should be positive.</param>
        /// <param name="sampleMinimum">The sample value corresponding to the minimum of the chirp impulse, should be positive.</param>
        /// <param name="initialPeriod">The instantaneous initial period of the chirp in samples, should be ≥ 2.</param>
        /// <param name="finalPeriod">The instantaneous final period of the chirp in samples, should be ≥ 2.</param>
        /// <param name="chirpSweep">The chirp sweep.</param>
        /// <param name="phaseInPi">The initial phase, φ, of the chirp in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].</param>
        /// <param name="isBiDirectional">If true, the period (or frequency) of even chirps descends from the final period (or frequency) to the initial one, to form a symmetrical shape with odd chirps.</param>
        protected ChirpDataGenerator(
            TimeSpan sessionBeginTime,
            TimeSpan sessionEndTime,
            DateTime startDate,
            TimeGranularity timeGranularity,
            BusinessDayCalendar businessDayCalendar,
            int waveformSamples,
            int offsetSamples,
            int repetitionsCount,
            double noiseAmplitudeFraction,
            IRandomGenerator noiseRandomGenerator,
            double sampleAmplitude,
            double sampleMinimum,
            double initialPeriod,
            double finalPeriod,
            ChirpSweep chirpSweep,
            double phaseInPi,
            bool isBiDirectional)
            : base(
                sessionBeginTime,
                sessionEndTime,
                startDate,
                timeGranularity,
                businessDayCalendar,
                waveformSamples,
                offsetSamples,
                repetitionsCount,
                noiseAmplitudeFraction,
                noiseRandomGenerator)
        {
            SampleAmplitude = sampleAmplitude;
            SampleMinimum = sampleMinimum;
            summand = sampleMinimum + sampleAmplitude;
            InitialPeriod = initialPeriod;
            instantPeriod = initialPeriod;
            initialFrequency = Constants.TwoPi / initialPeriod;
            instantFrequency = initialFrequency;
            FinalPeriod = finalPeriod;
            finalFrequency = Constants.TwoPi / finalPeriod;
            ChirpSweep = chirpSweep;
            IsBiDirectional = isBiDirectional;
            phase = Constants.Pi * phaseInPi;
            PhaseInPi = phaseInPi;
            angle = phase;
            lengthMinusOne = waveformSamples - 1;
            ratio = CalculateRatio(chirpSweep);
            Initialize();
        }

        private double CalculateRatio(ChirpSweep chirpSweep)
        {
            return chirpSweep switch
            {
                ChirpSweep.LinearPeriod => ((FinalPeriod - InitialPeriod) / lengthMinusOne),
                ChirpSweep.LinearFrequency => ((finalFrequency - initialFrequency) / lengthMinusOne),
                ChirpSweep.QuadraticPeriod => ((FinalPeriod - InitialPeriod) / (lengthMinusOne * lengthMinusOne)),
                ChirpSweep.QuadraticFrequency => ((finalFrequency - initialFrequency) / (lengthMinusOne * lengthMinusOne)),
                ChirpSweep.LogarithmicPeriod => (FinalPeriod / InitialPeriod),
                ChirpSweep.LogarithmicFrequency => (finalFrequency / initialFrequency),
                _ => throw new ArgumentException($"Unknown chirp sweep ${chirpSweep}", nameof(chirpSweep))
            };
        }

        private static string SweepName(ChirpSweep chirpSweep)
        {
            return chirpSweep switch
            {
                ChirpSweep.LinearPeriod => "linear period",
                ChirpSweep.LinearFrequency => "linear freq",
                ChirpSweep.QuadraticPeriod => "quadratic period",
                ChirpSweep.QuadraticFrequency => "quadratic freq",
                ChirpSweep.LogarithmicPeriod => "logarithmic period",
                ChirpSweep.LogarithmicFrequency => "logarithmic freq",
                _ => throw new ArgumentException($"Unknown chirp shape ${chirpSweep}", nameof(chirpSweep))
            };
        }

        private void Initialize()
        {
            Moniker = string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.####}∙chirp({1:0.####} ➜ {2:0.####}, {3:0.####}, {4}{5}",
                SampleAmplitude,
                InitialPeriod,
                FinalPeriod,
                WaveformSamples,
                SweepName(ChirpSweep),
                IsBiDirectional ? ", bidirectional" : string.Empty);

            const double delta = 0.00005;
            Moniker = Math.Abs(PhaseInPi) < delta
                ? string.Format(CultureInfo.InvariantCulture, "{0}) + {1:0.####}", Moniker, SampleMinimum)
                : string.Format(CultureInfo.InvariantCulture, "{0}, {1:0.####}∙π) + {2:0.####}", Moniker, PhaseInPi, SampleMinimum);

            if (HasNoise && NoiseAmplitudeFraction > delta)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0} + noise(ρn={1:0.####})", Moniker, NoiseAmplitudeFraction);

            if (OffsetSamples > 0)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, off={1}", Moniker, OffsetSamples);

            if (!IsRepetitionsInfinite)
                Moniker = string.Format(CultureInfo.InvariantCulture, "{0}, rep={1}", Moniker, RepetitionsCount);
        }

        /// <inheritdoc />
        protected override double OutOfWaveformSample()
        {
            return summand;
        }

        /// <inheritdoc />
        protected override double NextSample()
        {
            if (IsBiDirectional)
            {
                instantFrequency = directionUp ? NextFrequencyUp() : NextFrequencyDown();
                if (CurrentSampleIndex == WaveformSamples)
                    directionUp = !directionUp;
            }
            else
            {
                instantFrequency = NextFrequencyUp();
            }

            angle += instantFrequency;
            if (angle > Constants.TwoPi)
                angle -= Constants.TwoPi;
            else if (angle < -Constants.TwoPi)
                angle += Constants.TwoPi;
            return summand + SampleAmplitude * Math.Cos(angle);
        }

        private double NextFrequencyUp()
        {
            if (CurrentSampleIndex == 1)
                return initialFrequency;
            int n;
            switch (ChirpSweep)
            {
                case ChirpSweep.LinearPeriod:
                    instantPeriod += ratio;
                    return Constants.TwoPi / instantPeriod;
                case ChirpSweep.LinearFrequency:
                    return instantFrequency + ratio;
                case ChirpSweep.QuadraticPeriod:
                    n = CurrentSampleIndex - 1;
                    instantPeriod = InitialPeriod + ratio * n * n;
                    return Constants.TwoPi / instantPeriod;
                case ChirpSweep.QuadraticFrequency:
                    n = CurrentSampleIndex - 1;
                    return initialFrequency + ratio * n * n;
                case ChirpSweep.LogarithmicPeriod:
                    instantPeriod = InitialPeriod * Math.Pow(ratio, (CurrentSampleIndex - 1) / lengthMinusOne);
                    return Constants.TwoPi / instantPeriod;
                case ChirpSweep.LogarithmicFrequency:
                    return initialFrequency * Math.Pow(ratio, (CurrentSampleIndex - 1) / lengthMinusOne);
            }

            return initialFrequency;
        }

        private double NextFrequencyDown()
        {
            if (CurrentSampleIndex == 1)
                return finalFrequency;
            int n;
            switch (ChirpSweep)
            {
                case ChirpSweep.LinearPeriod:
                    instantPeriod -= ratio;
                    return Constants.TwoPi / instantPeriod;
                case ChirpSweep.LinearFrequency:
                    return instantFrequency - ratio;
                case ChirpSweep.QuadraticPeriod:
                    n = WaveformSamples - CurrentSampleIndex + 1;
                    instantPeriod = InitialPeriod + ratio * n * n;
                    return Constants.TwoPi / instantPeriod;
                case ChirpSweep.QuadraticFrequency:
                    n = WaveformSamples - CurrentSampleIndex + 1;
                    return initialFrequency + ratio * n * n;
                case ChirpSweep.LogarithmicPeriod:
                    instantPeriod = InitialPeriod * Math.Pow(ratio, (WaveformSamples - CurrentSampleIndex + 1) / lengthMinusOne);
                    return Constants.TwoPi / instantPeriod;
                case ChirpSweep.LogarithmicFrequency:
                    return initialFrequency * Math.Pow(ratio, (WaveformSamples - CurrentSampleIndex + 1) / lengthMinusOne);
            }

            return finalFrequency;
        }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            angle = phase;
            instantFrequency = initialFrequency;
            instantPeriod = InitialPeriod;
            directionUp = true;
        }
    }
}
