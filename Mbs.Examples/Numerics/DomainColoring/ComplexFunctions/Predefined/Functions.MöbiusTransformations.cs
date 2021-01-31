using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using Mbs.Numerics;

namespace DomainColoring.ComplexFunctions.Predefined
{
    /// <summary>
    /// A collection of Möbius transformations.
    /// </summary>
    internal static partial class Functions
    {
        public static IEnumerable<ComplexFunction> GetMöbiusTransformations()
        {
            const double tStart = 0;
            const double tEnd = 80;
            const double dStart = 0.001;
            const double dEnd = 0.999;
            const double ratio = (dEnd - dStart) / (tEnd - tStart);

            for (double t = tStart; t < tEnd;)
            {
                double c = t;
                t += dStart + ratio * (t - tStart);
                yield return new ComplexFunction
                {
                    Label = $"t = {c}", Function = z => MöbiusTransform(z, c),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                };
            }
        }

        // https://en.wikipedia.org/wiki/Möbius_transformation
        public static IEnumerable<ComplexFunction> GetMöbiusTransformations0()
        {
            return new[]
            {
                new ComplexFunction
                {
                    Label = "t = 0.001", Function = z => MöbiusTransform(z, 0.001),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.002", Function = z => MöbiusTransform(z, 0.002),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.003", Function = z => MöbiusTransform(z, 0.003),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.004", Function = z => MöbiusTransform(z, 0.004),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.005", Function = z => MöbiusTransform(z, 0.005),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.006", Function = z => MöbiusTransform(z, 0.006),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.007", Function = z => MöbiusTransform(z, 0.007),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.008", Function = z => MöbiusTransform(z, 0.008),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.009", Function = z => MöbiusTransform(z, 0.009),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.010", Function = z => MöbiusTransform(z, 0.010),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.015", Function = z => MöbiusTransform(z, 0.015),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.020", Function = z => MöbiusTransform(z, 0.020),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.025", Function = z => MöbiusTransform(z, 0.025),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.030", Function = z => MöbiusTransform(z, 0.030),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.035", Function = z => MöbiusTransform(z, 0.035),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.040", Function = z => MöbiusTransform(z, 0.040),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.045", Function = z => MöbiusTransform(z, 0.045),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.050", Function = z => MöbiusTransform(z, 0.050),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.055", Function = z => MöbiusTransform(z, 0.055),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.060", Function = z => MöbiusTransform(z, 0.060),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.065", Function = z => MöbiusTransform(z, 0.065),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.070", Function = z => MöbiusTransform(z, 0.070),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.075", Function = z => MöbiusTransform(z, 0.075),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.080", Function = z => MöbiusTransform(z, 0.080),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.085", Function = z => MöbiusTransform(z, 0.085),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.090", Function = z => MöbiusTransform(z, 0.090),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.095", Function = z => MöbiusTransform(z, 0.095),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.100", Function = z => MöbiusTransform(z, 0.100),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.110", Function = z => MöbiusTransform(z, 0.110),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.120", Function = z => MöbiusTransform(z, 0.120),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.130", Function = z => MöbiusTransform(z, 0.130),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.140", Function = z => MöbiusTransform(z, 0.140),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.150", Function = z => MöbiusTransform(z, 0.150),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.160", Function = z => MöbiusTransform(z, 0.160),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.170", Function = z => MöbiusTransform(z, 0.170),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.180", Function = z => MöbiusTransform(z, 0.180),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.190", Function = z => MöbiusTransform(z, 0.190),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.200", Function = z => MöbiusTransform(z, 0.200),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.210", Function = z => MöbiusTransform(z, 0.210),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.220", Function = z => MöbiusTransform(z, 0.220),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.230", Function = z => MöbiusTransform(z, 0.230),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.240", Function = z => MöbiusTransform(z, 0.240),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.250", Function = z => MöbiusTransform(z, 0.250),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.260", Function = z => MöbiusTransform(z, 0.260),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.270", Function = z => MöbiusTransform(z, 0.270),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.280", Function = z => MöbiusTransform(z, 0.280),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.290", Function = z => MöbiusTransform(z, 0.290),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.300", Function = z => MöbiusTransform(z, 0.300),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.310", Function = z => MöbiusTransform(z, 0.310),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.320", Function = z => MöbiusTransform(z, 0.320),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.330", Function = z => MöbiusTransform(z, 0.330),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.340", Function = z => MöbiusTransform(z, 0.340),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.350", Function = z => MöbiusTransform(z, 0.350),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.360", Function = z => MöbiusTransform(z, 0.360),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.370", Function = z => MöbiusTransform(z, 0.370),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.380", Function = z => MöbiusTransform(z, 0.380),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.390", Function = z => MöbiusTransform(z, 0.390),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.400", Function = z => MöbiusTransform(z, 0.400),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.410", Function = z => MöbiusTransform(z, 0.410),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.420", Function = z => MöbiusTransform(z, 0.420),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.430", Function = z => MöbiusTransform(z, 0.430),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.440", Function = z => MöbiusTransform(z, 0.440),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.450", Function = z => MöbiusTransform(z, 0.450),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.460", Function = z => MöbiusTransform(z, 0.460),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.470", Function = z => MöbiusTransform(z, 0.470),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.480", Function = z => MöbiusTransform(z, 0.480),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.490", Function = z => MöbiusTransform(z, 0.490),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.500", Function = z => MöbiusTransform(z, 0.500),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.510", Function = z => MöbiusTransform(z, 0.510),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.520", Function = z => MöbiusTransform(z, 0.520),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.530", Function = z => MöbiusTransform(z, 0.530),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.540", Function = z => MöbiusTransform(z, 0.540),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.550", Function = z => MöbiusTransform(z, 0.550),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.560", Function = z => MöbiusTransform(z, 0.560),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.570", Function = z => MöbiusTransform(z, 0.570),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.580", Function = z => MöbiusTransform(z, 0.580),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.590", Function = z => MöbiusTransform(z, 0.590),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.600", Function = z => MöbiusTransform(z, 0.600),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.610", Function = z => MöbiusTransform(z, 0.610),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.620", Function = z => MöbiusTransform(z, 0.620),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.630", Function = z => MöbiusTransform(z, 0.630),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.640", Function = z => MöbiusTransform(z, 0.640),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.650", Function = z => MöbiusTransform(z, 0.650),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.660", Function = z => MöbiusTransform(z, 0.660),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.670", Function = z => MöbiusTransform(z, 0.670),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.680", Function = z => MöbiusTransform(z, 0.680),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.690", Function = z => MöbiusTransform(z, 0.690),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.700", Function = z => MöbiusTransform(z, 0.700),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.710", Function = z => MöbiusTransform(z, 0.710),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.720", Function = z => MöbiusTransform(z, 0.720),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.730", Function = z => MöbiusTransform(z, 0.730),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.740", Function = z => MöbiusTransform(z, 0.740),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.750", Function = z => MöbiusTransform(z, 0.750),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.760", Function = z => MöbiusTransform(z, 0.760),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.770", Function = z => MöbiusTransform(z, 0.770),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.780", Function = z => MöbiusTransform(z, 0.780),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.790", Function = z => MöbiusTransform(z, 0.790),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.800", Function = z => MöbiusTransform(z, 0.800),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.810", Function = z => MöbiusTransform(z, 0.810),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.820", Function = z => MöbiusTransform(z, 0.820),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.830", Function = z => MöbiusTransform(z, 0.830),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.840", Function = z => MöbiusTransform(z, 0.840),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.850", Function = z => MöbiusTransform(z, 0.850),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.860", Function = z => MöbiusTransform(z, 0.860),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.870", Function = z => MöbiusTransform(z, 0.870),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.880", Function = z => MöbiusTransform(z, 0.880),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.890", Function = z => MöbiusTransform(z, 0.890),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.900", Function = z => MöbiusTransform(z, 0.900),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.910", Function = z => MöbiusTransform(z, 0.910),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.920", Function = z => MöbiusTransform(z, 0.920),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.930", Function = z => MöbiusTransform(z, 0.930),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.940", Function = z => MöbiusTransform(z, 0.940),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.950", Function = z => MöbiusTransform(z, 0.950),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.960", Function = z => MöbiusTransform(z, 0.960),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.970", Function = z => MöbiusTransform(z, 0.970),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.980", Function = z => MöbiusTransform(z, 0.980),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.990", Function = z => MöbiusTransform(z, 0.990),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.991", Function = z => MöbiusTransform(z, 0.991),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.992", Function = z => MöbiusTransform(z, 0.992),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.993", Function = z => MöbiusTransform(z, 0.993),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.994", Function = z => MöbiusTransform(z, 0.994),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.995", Function = z => MöbiusTransform(z, 0.995),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.996", Function = z => MöbiusTransform(z, 0.996),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.997", Function = z => MöbiusTransform(z, 0.997),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.998", Function = z => MöbiusTransform(z, 0.998),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.999", Function = z => MöbiusTransform(z, 0.999),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9991", Function = z => MöbiusTransform(z, 0.9991),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9992", Function = z => MöbiusTransform(z, 0.9992),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9993", Function = z => MöbiusTransform(z, 0.9993),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9994", Function = z => MöbiusTransform(z, 0.9994),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9995", Function = z => MöbiusTransform(z, 0.9995),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9996", Function = z => MöbiusTransform(z, 0.9996),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9997", Function = z => MöbiusTransform(z, 0.9997),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9998", Function = z => MöbiusTransform(z, 0.9998),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9999", Function = z => MöbiusTransform(z, 0.9999),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.99999", Function = z => MöbiusTransform(z, 0.99999),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.999999", Function = z => MöbiusTransform(z, 0.999999),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 0.9999999", Function = z => MöbiusTransform(z, 0.9999999),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 1", Function = z => MöbiusTransform(z, 1),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 1.1", Function = z => MöbiusTransform(z, 1.1),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 2.1", Function = z => MöbiusTransform(z, 2.1),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 3.1", Function = z => MöbiusTransform(z, 3.1),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
                new ComplexFunction
                {
                    Label = "t = 10.1", Function = z => MöbiusTransform(z, 10.1),
                    ReMin = -C2Pi, ReMax = C2Pi, ImMin = -C2Pi, ImMax = C2Pi,
                },
            };
        }

        private static Complex MöbiusTransform(Complex z, double t)
        {
            const double oneOver4Pi = 1 / (Constants.TwoPi * Constants.TwoPi);
            Complex fz = z - Complex.ImaginaryOne * t;
            fz /= 1d + t * (1d + Complex.ImaginaryOne * oneOver4Pi - Constants.Pi * z);
            return fz;
        }
    }
}
