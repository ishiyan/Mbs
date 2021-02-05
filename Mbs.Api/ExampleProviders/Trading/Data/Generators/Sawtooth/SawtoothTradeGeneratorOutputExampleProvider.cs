using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth
{
    /// <inheritdoc />
    internal class SawtoothTradeGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Trade>>
    {
        internal const string Name = SawtoothTradeGenerator.WaveformName;
        internal const string Moniker = "100∙sawtooth(128, linear) + 10 + noise(ρn=0.01), v=100";

        internal const double Price1 = 9.98;
        internal const double Price2 = 10.84;

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
                    new Trade(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Price2, DefaultParameterValues.TradeVolume),
                },
            };
        }
    }
}
