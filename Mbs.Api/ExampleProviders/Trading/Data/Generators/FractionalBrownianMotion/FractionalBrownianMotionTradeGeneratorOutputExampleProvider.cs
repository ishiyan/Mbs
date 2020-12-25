using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <inheritdoc />
    internal class FractionalBrownianMotionTradeGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Trade>>
    {
        internal const string Name = FractionalBrownianMotionTradeGenerator.WaveformName;
        internal const string Moniker = "fBm(Hosking, l=128, H=0.63) ∙ 100 + 10 + noise(ρn=0.01), v=100";

        internal const double Price1 = 14.21;
        internal const double Price2 = 19.15;

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
