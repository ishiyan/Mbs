using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    /// <inheritdoc />
    internal class MultiSinusoidalOhlcvGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Ohlcv>>
    {
        internal const string Name = MultiSinusoidalOhlcvGenerator.WaveformName;
        internal const string Moniker = "10∙cos(2π∙t/16) + 10 + noise(ρn=0.01), v=100, ρb=0.2, ρs=0.3";

        internal const double Open1 = 23.95;
        internal const double High1 = 38.92;
        internal const double Low1 = 20.96;
        internal const double Close1 = 35.93;

        internal const double Open2 = 35.27;
        internal const double High2 = 38.21;
        internal const double Low2 = 20.57;
        internal const double Close2 = 23.51;

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
                    new Ohlcv(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Open2, High2, Low2, Close2, DefaultParameterValues.CandlestickVolume),
                },
            };
        }
    }
}
