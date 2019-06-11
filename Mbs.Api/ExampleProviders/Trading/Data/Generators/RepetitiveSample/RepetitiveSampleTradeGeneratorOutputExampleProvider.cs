using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.RepetitiveSample;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    /// <inheritdoc />
    internal class RepetitiveSampleTradeGeneratorOutputExampleProvider : IExamplesProvider
    {
        internal const string Name = RepetitiveSampleTradeGenerator.WaveformName;
        internal const string Moniker = "repetitive trade sample (len=252)";

        internal const double Price1 = 91.5;
        internal const double Volume1 = 4077500;

        internal const double Price2 = 94.81;
        internal const double Volume2 = 4955900;

        /// <inheritdoc />
        public object GetExamples()
        {
            return new SyntheticDataGeneratorOutput<Trade>
            {
                Name = Name,
                Moniker = Moniker,
                Data = new[]
                {
                    new Trade(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), Price1, Volume1),
                    new Trade(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Price2, Volume2)
                }
            };
        }
    }
}
