using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Square;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Square
{
    /// <inheritdoc />
    internal class SquareTradeGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Trade>>
    {
        internal const string Name = SquareTradeGenerator.WaveformName;
        internal const string Moniker = "100∙square(128) + 10 + noise(ρn=0.01), v=100";

        internal const double Price1 = 109.79;
        internal const double Price2 = 110.58;

        /// <inheritdoc />
        public SyntheticDataGeneratorOutput<Trade> GetExamples()
        {
            return new SyntheticDataGeneratorOutput<Trade>
            {
                Name = Name,
                Moniker = Moniker,
                Data = new[]
                {
                    new Trade(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), Price1, DefaultParameterValues.TradeVolume),
                    new Trade(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Price2, DefaultParameterValues.TradeVolume)
                }
            };
        }
    }
}
