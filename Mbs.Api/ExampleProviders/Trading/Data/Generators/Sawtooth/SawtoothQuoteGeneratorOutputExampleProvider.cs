using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth
{
    /// <inheritdoc />
    internal class SawtoothQuoteGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Quote>>
    {
        internal const string Name = SawtoothQuoteGenerator.WaveformName;
        internal const string Moniker = "100∙sawtooth(128, linear) + 10 + noise(ρn=0.01), ρs=0.1, as=10, bs=10";

        internal const double BidPrice1 = 8.98;
        internal const double AskPrice1 = 10.97;

        internal const double BidPrice2 = 9.76;
        internal const double AskPrice2 = 11.92;

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
