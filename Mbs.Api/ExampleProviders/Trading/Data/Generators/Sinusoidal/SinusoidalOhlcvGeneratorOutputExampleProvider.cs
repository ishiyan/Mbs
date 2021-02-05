using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sinusoidal
{
    /// <inheritdoc />
    internal class SinusoidalOhlcvGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Ohlcv>>
    {
        internal const string Name = SinusoidalOhlcvGenerator.WaveformName;
        internal const string Moniker = "100∙cos(2π∙t/16) + 10 + noise(ρn=0.01), v=100, ρb=0.2, ρs=0.3";

        internal const double Open1 = 167.68;
        internal const double High1 = 272.48;
        internal const double Low1 = 146.72;
        internal const double Close1 = 251.52;

        internal const double Open2 = 244.16;
        internal const double High2 = 264.51;
        internal const double Low2 = 142.43;
        internal const double Close2 = 162.77;

        /// <inheritdoc />
        public SyntheticDataGeneratorOutput<Ohlcv> GetExamples()
        {
            return new SyntheticDataGeneratorOutput<Ohlcv>
            {
                Name = Name,
                Moniker = Moniker,
                Data = new[]
                {
                    new Ohlcv(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), Open1, High1, Low1, Close1, DefaultParameterValues.CandlestickVolume),
                    new Ohlcv(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Open2, High2, Low2,  Close2, DefaultParameterValues.CandlestickVolume),
                },
            };
        }
    }
}
