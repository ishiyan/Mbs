using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    /// <inheritdoc />
    internal class GeometricBrownianMotionTradeGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Trade>>
    {
        internal const string Name = GeometricBrownianMotionTradeGenerator.WaveformName;
        internal const string Moniker = "gBm(l=128, μ=0.003, σ=0.3) ∙ 100 + 10 + noise(ρn=0.01), v=100";

        internal const double Price1 = 15.40;
        internal const double Price2 = 21.88;

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
