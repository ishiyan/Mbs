using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sinusoidal
{
    /// <inheritdoc />
    internal class SinusoidalQuoteGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Quote>>
    {
        internal const string Name = SinusoidalQuoteGenerator.WaveformName;
        internal const string Moniker = "100∙cos(2π∙t/16) + 10 + noise(ρn=0.01), ρs=0.1, as=10, bs=10";

        internal const double BidPrice1 = 188.64;
        internal const double AskPrice1 = 230.56;

        internal const double BidPrice2 = 183.12;
        internal const double AskPrice2 = 223.82;

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
