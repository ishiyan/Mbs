using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth
{
    /// <inheritdoc />
    internal class SawtoothOhlcvGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Ohlcv>>
    {
        internal const string Name = SawtoothOhlcvGenerator.WaveformName;
        internal const string Moniker = "100∙sawtooth(128, linear) + 10 + noise(ρn=0.01), v=100, ρb=0.2, ρs=0.3";

        internal const double Open1 = 7.98;
        internal const double High1 = 12.97;
        internal const double Low1 = 6.98;
        internal const double Close1 = 11.97;

        internal const double Open2 = 8.67;
        internal const double High2 = 14.09;
        internal const double Low2 = 7.59;
        internal const double Close2 = 13.01;

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
