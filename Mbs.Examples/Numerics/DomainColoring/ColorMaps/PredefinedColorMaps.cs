using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace DomainColoring.ColorMaps
{
    /// <summary>
    /// A collection of various color mapping techniques.
    /// </summary>
    internal static partial class PredefinedColorMaps
    {
        public static IEnumerable<ColorMap> Get()
        {
            return new[]
            {
                new ColorMap
                {
                    Label = " Claudio Rocchini ", Function = ClaudioRocchini,
                },
                new ColorMap
                {
                    Label = " Rainbow ", Function = Rainbow,
                },
                new ColorMap
                {
                    Label = " Wind Wish ", Function = WindWish,
                },
                new ColorMap
                {
                    Label = " Natural ", Function = Natural,
                },
            };
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int HsvToBrga32(double h, double s, double v)
        {
            double r, g, b;
            if (s == 0)
            {
                r = g = b = v;
            }
            else
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (h == 1)
                {
                    h = 0;
                }

                double w = Math.Truncate(6 * h);
                double f = h * 6 - w;
                double p = v * (1 - s);
                double q = v * (1 - s * f);
                double t = v * (1 - s * (1 - f));

                switch ((int)w)
                {
                    case 0:
                        r = v; g = t; b = p; break;
                    case 1:
                        r = q; g = v; b = p; break;
                    case 2:
                        r = p; g = v; b = t; break;
                    case 3:
                        r = p; g = q; b = v; break;
                    case 4:
                        r = t; g = p; b = v; break;
                    case 5:
                        r = v; g = p; b = q; break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return (0xff << 24)
                   | ((int)Math.Truncate(255d * r) << 16)
                   | ((int)Math.Truncate(255d * g) << 8)
                   | (int)Math.Truncate(255d * b);
        }
    }
}
