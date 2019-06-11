using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    /// <inheritdoc />
    internal class MultiSinusoidalQuoteGeneratorOutputExampleProvider : IExamplesProvider
    {
        internal const string Name = MultiSinusoidalQuoteGenerator.WaveformName;
        internal const string Moniker = "10∙cos(2π∙t/16) + 10 + noise(ρn=0.01), v=100, ρb=0.2, ρs=0.3";

        internal const double BidPrice1 = 26.94;
        internal const double AskPrice1 = 32.93;

        internal const double BidPrice2 = 26.45;
        internal const double AskPrice2 = 32.33;

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
