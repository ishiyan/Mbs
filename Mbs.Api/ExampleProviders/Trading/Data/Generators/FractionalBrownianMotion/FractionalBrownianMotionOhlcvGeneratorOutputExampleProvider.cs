using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <inheritdoc />
    internal class FractionalBrownianMotionOhlcvGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Ohlcv>>
    {
        internal const string Name = FractionalBrownianMotionOhlcvGenerator.WaveformName;
        internal const string Moniker = "fBm(Hosking, l=128, H=0.63) ∙ 100 + 10 + noise(ρn=0.01), v=100, ρb=0.2, ρs=0.3";

        internal const double Open1 = 11.37;
        internal const double High1 = 18.47;
        internal const double Low1 = 9.94;
        internal const double Close1 = 17.05;

        internal const double Open2 = 15.32;
        internal const double High2 = 24.90;
        internal const double Low2 = 13.40;
        internal const double Close2 = 22.98;

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
