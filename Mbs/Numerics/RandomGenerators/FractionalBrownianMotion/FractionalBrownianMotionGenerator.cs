using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// Generates fractional Brownian motion samples.
    /// <para/>Converted from http://www.columbia.edu/~ad3217/fbm.html
    /// <para/>Ton Dieker, Centre of Mathematics and Computer Science (CWI) Amsterdam, 2002.
    /// </summary>
    public static class FractionalBrownianMotionGenerator
    {
        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the Hosking method.
        /// <para/>Reference:
        /// <para/>J.R.M. Hosking (1984),
        /// <para/>Modeling persistence in hydrological time series using fractional brownian differencing,
        /// <para/>Water Resources Research, Vol. 20, pp. 1898--1908.
        /// </summary>
        /// <param name="nrg">The normal random generator interface.</param>
        /// <param name="outputLength">The number of samples N to generate, N = 2ⁿ.</param>
        /// <param name="output">The output array of size N, N = 2ⁿ.</param>
        /// <param name="maxValue">The sample is generated on [0, maxValue].</param>
        /// <param name="hurstExponent">The Hurst parameter.</param>
        /// <param name="isCumulative">If true, generates fractional Brownian motion, otherwise generates fractional Gaussian noise.</param>
        public static void GenerateHosking(INormalRandomGenerator nrg, int outputLength, double[] output, double maxValue, double hurstExponent, bool isCumulative)
        {
            double[] phi = new double[outputLength];
            double[] cov = new double[outputLength];
            double[] psi = new double[outputLength];

            // Initialization.
            output[0] = nrg.NextDoubleStandard();
            double v = 1;
            phi[0] = 0;
            cov[0] = 1;
            double twoH = 2 * hurstExponent;
            for (int i = 1; i < outputLength; ++i)
                cov[i] = Covariance(i, twoH);

            // Simulation.
            for (int i = 1; i < outputLength; ++i)
            {
                int i1 = i - 1;
                phi[i1] = cov[i];
                for (int j = 0; j < i1; ++j)
                {
                    psi[j] = phi[j];
                    phi[i1] -= psi[j] * cov[i1 - j];
                }

                phi[i1] /= v;
                for (int j = 0; j < i1; ++j)
                {
                    phi[j] = psi[j] - phi[i1] * psi[i1 - j - 1];
                }

                v *= 1 - phi[i1] * phi[i1];
                output[i] = 0;
                for (int j = 0; j < i; ++j)
                {
                    output[i] += phi[j] * output[i1 - j];
                }

                output[i] += Math.Sqrt(v) * nrg.NextDoubleStandard();
            }

            // Rescale to obtain a sample of size 2ⁿ on [0, maxValue].
            double scaling = Math.Pow(maxValue / outputLength, hurstExponent);
            output[0] *= scaling;
            if (isCumulative)
            {
                for (int i = 1; i < outputLength; ++i)
                {
                    output[i] *= scaling;
                    output[i] += output[i - 1];
                }
            }
            else
            {
                for (int i = 1; i < outputLength; ++i)
                {
                    output[i] *= scaling;
                }
            }
        }

        /// <summary>
        /// The autocovariance function of a fractional Gaussian noise.
        /// </summary>
        /// <param name="i">The index number, i > 0. For i == 1 the return value is 1.</param>
        /// <param name="twoH">The value of the Hurst exponent times two.</param>
        /// <returns>The value of the calculated covariance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double Covariance(int i, double twoH)
        {
            return (Math.Pow(i - 1, twoH) - 2 * Math.Pow(i, twoH) + Math.Pow(i + 1, twoH)) / 2;
        }

        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the approximate Paxson method.
        /// </summary>
        /// <param name="nrg">The normal random generator interface.</param>
        /// <param name="outputLength">The number of samples N to generate, N = 2ⁿ.</param>
        /// <param name="output">The output array of size N, N = 2ⁿ.</param>
        /// <param name="maxValue">The sample is generated on [0, maxValue].</param>
        /// <param name="hurstExponent">The Hurst parameter.</param>
        /// <param name="isCumulative">If true, generates fractional Brownian motion, otherwise generates fractional Gaussian noise.</param>
        public static void GeneratePaxson(INormalRandomGenerator nrg, int outputLength, double[] output, double maxValue, double hurstExponent, bool isCumulative)
        {
            int halfOutputLength = outputLength / 2;
            double[] powerSpectrum = new double[halfOutputLength + 1];

            // Approximate spectral density.
            FractionalGaussianNoiseSpectrum(powerSpectrum, halfOutputLength, hurstExponent);

            Complex[] a = new Complex[outputLength];
            a[0].Real = 0;
            a[0].Imag = 0;
            for (int i = 1; i <= halfOutputLength; ++i)
            {
                double aux = Math.Sqrt(powerSpectrum[i]);
                a[i].Real = aux * nrg.NextDoubleStandard();
                a[i].Imag = aux * nrg.NextDoubleStandard();
            }

            for (int i = halfOutputLength + 1; i < outputLength; ++i)
            {
                a[i].Real = a[outputLength - i].Real;
                a[i].Imag = -a[outputLength - i].Imag;
            }

            // Real part of Fourier transform of a.Real + i a.Imag gives sample path.
            ForwardFft(outputLength, a);

            // Rescale to obtain a sample of size 2ⁿ on [0, maxValue].
            double scaling = Math.Pow(maxValue / outputLength, hurstExponent) / Math.Sqrt(2 * outputLength);
            output[0] = scaling * a[0].Real;
            if (isCumulative)
            {
                for (int i = 1; i < outputLength; ++i)
                {
                    output[i] = scaling * a[i].Real;
                    output[i] += output[i - 1];
                }
            }
            else
            {
                for (int i = 1; i < outputLength; ++i)
                {
                    output[i] = scaling * a[i].Real;
                }
            }
        }

        /// <summary>
        /// Calculates an approximation of the power spectrum for the fractional Gaussian noise at the given frequencies lambda and the given Hurst parameter.
        /// </summary>
        /// <param name="powerSpectrum">The power spectrum array.</param>
        /// <param name="length">The length of the power spectrum array.</param>
        /// <param name="hurst">The Hurst coefficient.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void FractionalGaussianNoiseSpectrum(double[] powerSpectrum, int length, double hurst)
        {
            // The result of the natural logrithm of the gamma function will always be positive.
            double g = SpecialFunctions.LnGamma(2 * hurst + 1);

            double factor = 2 * Math.Sin(Math.PI * hurst) * Math.Exp(g);
            double[] arrayA = new double[5];
            double[] arrayB = new double[5];
            for (int i = 1; i < length + 1; ++i)
            {
                double lambda = Math.PI * i / length;
                double a = factor * (1 - Math.Cos(lambda));
                double b = Math.Pow(lambda, -2 * hurst - 1);
                double c = FractionalGaussianNoiseEstimateAdjustedB(lambda, hurst, arrayA, arrayB);
                powerSpectrum[i] = a * (b + c);
            }
        }

        /// <summary>
        /// Calculates an adjusted estimate for B(lambda, hurst).
        /// </summary>
        /// <param name="lambda">The value of lambda.</param>
        /// <param name="hurst">The Hurst coefficient.</param>
        /// <param name="a">A first temporary array of size 5. The index 0 is never used!.</param>
        /// <param name="b">A second temporary array of size 5. The index 0 is never used!.</param>
        /// <returns>The estimated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double FractionalGaussianNoiseEstimateAdjustedB(double lambda, double hurst, double[] a, double[] b)
        {
            return (1.0002 - 0.000134 * lambda) * (FractionalGaussianNoiseEstimateB(lambda, hurst, a, b) - Math.Pow(2, -7.65 * hurst - 7.4));
        }

        /// <summary>
        /// Calculates an estimate for B(lambda, hurst).
        /// </summary>
        /// <param name="lambda">The value of lambda.</param>
        /// <param name="hurst">The Hurst coefficient.</param>
        /// <param name="a">A first temporary array of size 5. The index 0 is never used!.</param>
        /// <param name="b">A second temporary array of size 5. The index 0 is never used!.</param>
        /// <returns>The estimated value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double FractionalGaussianNoiseEstimateB(double lambda, double hurst, double[] a, double[] b)
        {
            for (int k = 1; k < 5; ++k)
            {
                double factor = 2 * k * Math.PI;
                a[k] = factor + lambda;
                b[k] = factor - lambda;
            }

            double d = -2 * hurst - 1;
            double sum = 0;
            for (int k = 1; k < 4; ++k)
            {
                sum += Math.Pow(a[k], d);
                sum += Math.Pow(b[k], d);
            }

            double dPrime = -2 * hurst;
            double sumPrime = 0.0;
            for (int k = 3; k < 5; ++k)
            {
                sumPrime += Math.Pow(a[k], dPrime);
                sumPrime += Math.Pow(b[k], dPrime);
            }

            return sum + sumPrime / (8 * Math.PI * hurst);
        }

        /// <summary>
        /// 1-d forward fast Fourier transform from Claerbout (1985) p. 70.
        /// </summary>
        /// <param name="length">The length of the data array, must be the power of two.</param>
        /// <param name="array">The complex data array.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ForwardFft(int length, Complex[] array)
        {
            int j = 0;
            for (int i = 0; i < length; ++i)
            {
                if (i <= j)
                {
                    double real = array[j].Real;
                    double imag = array[j].Imag;
                    array[j].Real = array[i].Real;
                    array[j].Imag = array[i].Imag;
                    array[i].Real = real;
                    array[i].Imag = imag;
                }

                int m = length / 2;
                while (j > m - 1)
                {
                    j -= m;
                    m /= 2;
                    if (m < 1)
                        break;
                }

                j += m;
            }

            // for (int i = 0; i < 8; ++i)
            //     System.Diagnostics.Trace.WriteLine($"step 1: {i}, re={array[i].Real}, im={array[i].Imag}");
            int k = 1;
            do
            {
                int istep = 2 * k;
                for (int m = 0; m < k; ++m)
                {
                    double arg = Math.PI * m / k;
                    double cos = Math.Cos(arg);
                    double sin = Math.Sin(arg);
                    for (int i = m; i < length; i += istep)
                    {
                        double real = cos * array[i + k].Real - sin * array[i + k].Imag;
                        double imag = sin * array[i + k].Real + cos * array[i + k].Imag;
                        array[i + k].Real = array[i].Real - real;
                        array[i + k].Imag = array[i].Imag - imag;
                        array[i].Real += real;
                        array[i].Imag += imag;
                    }
                }

                k = istep;
            }
            while (k < length);
        }

        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the Wood and Chan algorithm.
        /// <para/>
        /// <para/>References:
        /// <para/>➊ R.B. Davies and D.S. Harte (1987),
        /// <para/>Tests for Hurst effect, Biometrika,
        /// <para/>Vol. 74, pp. 95--102.
        /// <para/>➋ C.R. Dietrich and G.N. Newsam (1997),
        /// <para/>Fast and exact simulation of stationary Gaussian processes through circulant embedding of the covariance matrix,
        /// <para/>SIAM Journal Sci. Comput., Vol. 18, pp. 1088--1107.
        /// <para/>➌ A. Wood and G. Chan (1994),
        /// <para/>Simulation of Stationary Gaussian Processes in [0,1]ᵈ,
        /// <para/>Journal of Comp. and Graphical Statistics, Vol. 3, pp. 409--432.
        /// <para/>http://amstat.tandfonline.com/doi/abs/10.1080/10618600.1994.10474655#.Wi7kDUqnGTF.
        /// </summary>
        /// <param name="nrg">The normal random generator interface.</param>
        /// <param name="outputLength">The number of samples N to generate, N = 2ⁿ.</param>
        /// <param name="output">The output array of size N, N = 2ⁿ.</param>
        /// <param name="maxValue">The sample is generated on [0, maxValue].</param>
        /// <param name="hurstExponent">The Hurst parameter.</param>
        /// <param name="isCumulative">If true, generates fractional Brownian motion, otherwise generates fractional Gaussian noise.</param>
        public static void GenerateCirculant(INormalRandomGenerator nrg, int outputLength, double[] output, double maxValue, double hurstExponent, bool isCumulative)
        {
            // Complex[] test = new Complex[512];
            // for (int i = 0; i < test.Length; ++i)
            // {
            //     test[i].Real = i + 1;
            //     test[i].Imag = i + 2;
            // }

            // for (int i = 0; i < test.Length; ++i)
            //     System.Diagnostics.Trace.WriteLine($"{i}, re={test[i].Real}, im={test[i].Imag}");
            // ForwardFft(test.Length, test);
            // for (int i = 0; i < test.Length; ++i)
            //     System.Diagnostics.Trace.WriteLine($"{i}, re={test[i].Real}, im={test[i].Imag}");

            // Compute the eigenvalues of the circulant matrix.
            int outputLengthTwice = 2 * outputLength;
            Complex[] eigenvalues = new Complex[outputLengthTwice];
            ComputeEigenvalues(outputLength, outputLengthTwice, eigenvalues, hurstExponent);

            // Compute the input vectors for the FFT algorithm.
            Complex[] sAndT = new Complex[outputLengthTwice];
            ComputeSandt(nrg, outputLength, outputLengthTwice, eigenvalues, sAndT);

            // Real part of Fourier transform of S + iT gives sample path.
            ForwardFft(outputLengthTwice, sAndT);

            // Rescale to obtain a sample of size 2ⁿ on [0, maxValue].
            double scaling = Math.Pow(maxValue / outputLength, hurstExponent);
            output[0] = scaling * sAndT[0].Real;
            if (isCumulative)
            {
                for (int i = 1; i < outputLength; ++i)
                {
                    output[i] = scaling * sAndT[i].Real;
                    output[i] += output[i - 1];
                }
            }
            else
            {
                for (int i = 1; i < outputLength; ++i)
                {
                    output[i] = scaling * sAndT[i].Real;
                }
            }

            // for (int i = 0; i < outputLength; ++i)
            //     System.Diagnostics.Trace.WriteLine($"{i}, {output[i]}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ComputeEigenvalues(int length, int lengthTwice, Complex[] eigenvalues, double hurstExponent)
        {
            // Computes the eigenvalues of the circulant matrix that embeds the covariance matrix.
            // The eigenvalues array has length of lengthTwice.
            double hurstExponentTwice = 2 * hurstExponent;

            eigenvalues[0].Imag = 0;
            eigenvalues[0].Real = 1;
            for (int i = 1; i < lengthTwice; ++i)
            {
                eigenvalues[i].Imag = 0;
                eigenvalues[i].Real = i <= length ? Covariance(i, hurstExponentTwice) : eigenvalues[lengthTwice - i].Real;
            }

            // for (int i = 0; i < lengthTwice; ++i)
            //     System.Diagnostics.Trace.WriteLine($"{i}, re={eigenvalues[i].Real}, im={eigenvalues[i].Imag}");
            ForwardFft(lengthTwice, eigenvalues);

            // for (int i = 0; i < lengthTwice; ++i)
            //    System.Diagnostics.Trace.WriteLine($"{i}, re={eigenvalues[i].Real}, im={eigenvalues[i].Imag}");
            for (int i = 0; i < lengthTwice; ++i)
            {
                if (eigenvalues[i].Real <= 0)
                {
                    throw new NotSupportedException(
                        "The software should be modified to deal with this covariance function."
                        + Environment.NewLine
                        + "See A. Wood and G. Chan (1994),"
                        + Environment.NewLine
                        + "Simulation of Stationary Gaussian Processes in [0,1]ᵈ,"
                        + Environment.NewLine
                        + "Journal of Computational and Graphical Statistics, Vol. 3, pp. 409-432");
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ComputeSandt(INormalRandomGenerator nrg, int length, int lengthTwice, Complex[] eigenvalues, Complex[] sandt)
        {
            // Simulates two auxiliary vectors that serve as input in the FFT algorithm.
            // Both the eigenvalues and the sandt arrays have the length of lengthTwice.
            double sqrt = Math.Sqrt(lengthTwice);
            sandt[0].Real = Math.Sqrt(eigenvalues[0].Real) * nrg.NextDoubleStandard() / sqrt;
            sandt[0].Imag = 0;
            sandt[length].Real = Math.Sqrt(eigenvalues[length].Real) * nrg.NextDoubleStandard() / sqrt;
            sandt[length].Imag = 0;

            sqrt = Math.Sqrt(2 * lengthTwice);
            for (int i = 1; i < length; ++i)
            {
                sandt[i].Real = Math.Sqrt(eigenvalues[i].Real) * nrg.NextDoubleStandard() / sqrt;
                if (double.IsNaN(sandt[i].Real))
                    sandt[i].Real = double.Epsilon;
                sandt[i].Imag = Math.Sqrt(eigenvalues[i].Imag) * nrg.NextDoubleStandard() / sqrt;
                if (double.IsNaN(sandt[i].Imag))
                    sandt[i].Imag = double.Epsilon;
                sandt[lengthTwice - i].Real = sandt[i].Real;
                sandt[lengthTwice - i].Imag = -sandt[i].Imag;
            }
        }

        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the approximate circulant (Wood and Chan) algorithm.
        /// </summary>
        /// <param name="nrg">The normal random generator interface.</param>
        /// <param name="outputLength">The number of samples N to generate, N = 2ⁿ.</param>
        /// <param name="output">The output array of size N, N = 2ⁿ.</param>
        /// <param name="maxValue">The sample is generated on [0, maxValue].</param>
        /// <param name="hurstExponent">The Hurst parameter.</param>
        /// <param name="isCumulative">If true, generates fractional Brownian motion, otherwise generates fractional Gaussian noise.</param>
        public static void GenerateApproximateCirculant(INormalRandomGenerator nrg, int outputLength, double[] output, double maxValue, double hurstExponent, bool isCumulative)
        {
            int outputLengthTwice = 2 * outputLength;
            double[] powerSpectrum = new double[outputLength + 1];

            // Approximate spectral density.
            FractionalGaussianNoiseSpectrum(powerSpectrum, outputLength, hurstExponent);

            double hurstTwice = 2 * hurstExponent;
            Complex[] a = new Complex[outputLengthTwice];
            a[0].Real = Math.Sqrt(2 * (Math.Pow(outputLengthTwice, hurstTwice) - Math.Pow(outputLengthTwice - 1, hurstTwice))) * nrg.NextDoubleStandard();
            a[0].Imag = 0;
            a[outputLength].Real = Math.Sqrt(2 * powerSpectrum[outputLength]) * nrg.NextDoubleStandard();
            a[outputLength].Imag = 0;
            for (int i = 1; i < outputLength; ++i)
            {
                double aux = Math.Sqrt(powerSpectrum[i]);
                a[i].Real = aux * nrg.NextDoubleStandard();
                a[i].Imag = aux * nrg.NextDoubleStandard();
            }

            for (int i = outputLength + 1; i < outputLengthTwice; ++i)
            {
                a[i].Real = a[outputLengthTwice - i].Real;
                a[i].Imag = -a[outputLengthTwice - i].Imag;
            }

            // Real part of Fourier transform of a.Real + i a.Imag gives sample path.
            ForwardFft(outputLengthTwice, a);

            // Rescale to obtain a sample of size 2ⁿ on [0, maxValue].
            double scaling = Math.Pow(maxValue / outputLength, hurstExponent) / Math.Sqrt(2 * outputLengthTwice);
            output[0] = scaling * a[0].Real;
            if (isCumulative)
            {
                for (int i = 1; i < outputLength; ++i)
                {
                    output[i] = scaling * a[i].Real;
                    output[i] += output[i - 1];
                }
            }
            else
            {
                for (int i = 1; i < outputLength; ++i)
                {
                    output[i] = scaling * a[i].Real;
                }
            }
        }

        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the specified algorithm.
        /// </summary>
        /// <param name="algorithm">The fractional Brownian motion algorithm.</param>
        /// <param name="nrg">The normal random generator interface.</param>
        /// <param name="outputLength">The number of samples N to generate, N = 2ⁿ.</param>
        /// <param name="output">The output array of size N, N = 2ⁿ.</param>
        /// <param name="maxValue">The sample is generated on [0, maxValue].</param>
        /// <param name="hurstExponent">The Hurst parameter.</param>
        /// <param name="isCumulative">If true, generates fractional Brownian motion, otherwise generates fractional Gaussian noise.</param>
        public static void Generate(FractionalBrownianMotionAlgorithm algorithm, INormalRandomGenerator nrg, int outputLength, double[] output, double maxValue, double hurstExponent, bool isCumulative)
        {
            switch (algorithm)
            {
                case FractionalBrownianMotionAlgorithm.Hosking:
                    GenerateHosking(nrg, outputLength, output, maxValue, hurstExponent, isCumulative);
                    break;
                case FractionalBrownianMotionAlgorithm.Paxson:
                    GeneratePaxson(nrg, outputLength, output, maxValue, hurstExponent, isCumulative);
                    break;
                case FractionalBrownianMotionAlgorithm.ApproximateCirculant:
                    GenerateApproximateCirculant(nrg, outputLength, output, maxValue, hurstExponent, isCumulative);
                    break;
                case FractionalBrownianMotionAlgorithm.Circulant:
                    GenerateCirculant(nrg, outputLength, output, maxValue, hurstExponent, isCumulative);
                    break;
            }
        }
    }
}
