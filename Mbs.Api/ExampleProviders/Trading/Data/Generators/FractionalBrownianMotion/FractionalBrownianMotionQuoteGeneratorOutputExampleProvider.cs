using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <inheritdoc />
    internal class FractionalBrownianMotionQuoteGeneratorOutputExampleProvider : IExamplesProvider
    {
        internal const string Name = FractionalBrownianMotionQuoteGenerator.WaveformName;
        internal const string Moniker = "fBm(Hosking, l=128, H=0.63) ∙ 100 + 10 + noise(ρn=0.01), ρs=0.1, as=10, bs=10";

        internal const double BidPrice1 = 12.79;
        internal const double AskPrice1 = 15.63;

        internal const double BidPrice2 = 17.24;
        internal const double AskPrice2 = 21.07;

        /// <inheritdoc />
        public object GetExamples()
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
