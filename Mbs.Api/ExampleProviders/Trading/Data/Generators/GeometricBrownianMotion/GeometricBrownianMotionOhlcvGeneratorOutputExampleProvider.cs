using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    /// <inheritdoc />
    internal class GeometricBrownianMotionOhlcvGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Ohlcv>>
    {
        internal const string Name = GeometricBrownianMotionOhlcvGenerator.WaveformName;
        internal const string Moniker = "gBm(l=128, μ=0.003, σ=0.3) ∙ 100 + 10 + noise(ρn=0.01), v=100, ρb=0.2, ρs=0.3";

        internal const double Open1 = 12.32;
        internal const double High1 = 20.02;
        internal const double Low1 = 10.78;
        internal const double Close1 = 18.48;

        internal const double Open2 = 17.50;
        internal const double High2 = 28.45;
        internal const double Low2 = 15.32;
        internal const double Close2 = 26.26;

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
