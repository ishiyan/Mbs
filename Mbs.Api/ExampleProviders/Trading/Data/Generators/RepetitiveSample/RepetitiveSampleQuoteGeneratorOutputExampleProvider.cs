using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.RepetitiveSample;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    /// <inheritdoc />
    internal class RepetitiveSampleQuoteGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Quote>>
    {
        internal const string Name = RepetitiveSampleQuoteGenerator.WaveformName;
        internal const string Moniker = "repetitive quote sample (len=252)";

        internal const double BidPrice1 = 90.75;
        internal const double AskPrice1 = 93.25;
        internal const double BidSize1 = 4077500;
        internal const double AskSize1 = 4077500;

        internal const double BidPrice2 = 91.40;
        internal const double AskPrice2 = 94.94;
        internal const double BidSize2 = 4955900;
        internal const double AskSize2 = 4955900;

        /// <inheritdoc />
        public SyntheticDataGeneratorOutput<Quote> GetExamples()
        {
            return new SyntheticDataGeneratorOutput<Quote>
            {
                Name = Name,
                Moniker = Moniker,
                Data = new[]
                {
                    new Quote(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), BidPrice1, BidSize1, AskPrice1, AskSize1),
                    new Quote(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), BidPrice2, BidSize2, AskPrice2, AskSize2)
                }
            };
        }
    }
}
