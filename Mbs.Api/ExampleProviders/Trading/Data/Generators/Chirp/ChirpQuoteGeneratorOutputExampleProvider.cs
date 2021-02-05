using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Chirp
{
    /// <inheritdoc />
    internal class ChirpQuoteGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Quote>>
    {
        internal const string Name = ChirpQuoteGenerator.WaveformName;
        internal const string Moniker = "100∙chirp(128 ➜ 16, 128, linear period) + 10 + noise(ρn=0.01), ρs=0.1, as=10, bs=10";

        internal const double BidPrice1 = 188.53;
        internal const double AskPrice1 = 230.42;

        internal const double BidPrice2 = 189.57;
        internal const double AskPrice2 = 231.70;

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
                    new Quote(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), BidPrice2, DefaultParameterValues.BidSize, AskPrice2, DefaultParameterValues.AskSize),
                },
            };
        }
    }
}
