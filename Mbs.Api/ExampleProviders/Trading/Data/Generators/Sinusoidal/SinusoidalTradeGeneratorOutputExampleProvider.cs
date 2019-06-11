using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sinusoidal
{
    /// <inheritdoc />
    internal class SinusoidalTradeGeneratorOutputExampleProvider : IExamplesProvider
    {
        internal const string Name = SinusoidalTradeGenerator.WaveformName;
        internal const string Moniker = "100∙cos(2π∙t/16) + 10 + noise(ρn=0.01), v=100";

        internal const double Price1 = 209.60;
        internal const double Price2 = 203.47;

        /// <inheritdoc />
        public object GetExamples()
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
