using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.GeometricBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.GeometricBrownianMotion
{
    /// <inheritdoc />
    internal class GeometricBrownianMotionScalarGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Scalar>>
    {
        internal const string Name = GeometricBrownianMotionScalarGenerator.WaveformName;
        internal const string Moniker = "gBm(l=128, μ=0.003, σ=0.3) ∙ 100 + 10 + noise(ρn=0.01)";

        internal const double Price1 = 15.40;
        internal const double Price2 = 21.88;

        /// <inheritdoc />
        public SyntheticDataGeneratorOutput<Scalar> GetExamples()
        {
            return new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = Name,
                Moniker = Moniker,
                Data = new[]
                {
                    new Scalar(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), Price1),
                    new Scalar(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Price2),
                },
            };
        }
    }
}
