using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    /// <inheritdoc />
    internal class GeometricBrownianMotionQuoteGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Quote>>
    {
        internal const string Name = GeometricBrownianMotionQuoteGenerator.WaveformName;
        internal const string Moniker = "gBm(l=128, μ=0.003, σ=0.3) ∙ 100 + 10 + noise(ρn=0.01), ρs=0.1, as=10, bs=10";

        internal const double BidPrice1 = 13.86;
        internal const double AskPrice1 = 16.94;

        internal const double BidPrice2 = 19.69;
        internal const double AskPrice2 = 24.07;

        /// <inheritdoc />
        public SyntheticDataGeneratorOutput<Quote> GetExamples()
        {
            return new SyntheticDataGeneratorOutput<Quote>
            {
                Name = Name,
                Moniker = Moniker,
                Data = new[]
                {
                    new Quote(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), BidPrice1, DefaultParameterValues.BidSize, AskPrice1, DefaultParameterValues.AskSize),
                    new Quote(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), BidPrice2, DefaultParameterValues.BidSize, AskPrice2, DefaultParameterValues.AskSize)
                }
            };
        }
    }
}
