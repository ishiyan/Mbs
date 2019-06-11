using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.FractionalBrownianMotion;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.FractionalBrownianMotion
{
    /// <inheritdoc />
    internal class FractionalBrownianMotionScalarGeneratorOutputExampleProvider : IExamplesProvider
    {
        internal const string Name = FractionalBrownianMotionScalarGenerator.WaveformName;
        internal const string Moniker = "fBm(Hosking, l=128, H=0.63) ∙ 100 + 10 + noise(ρn=0.01)";

        internal const double Price1 = 14.21;
        internal const double Price2 = 19.15;

        /// <inheritdoc />
        public object GetExamples()
        {
            return new SyntheticDataGeneratorOutput<Scalar>
            {
                Name = Name,
                Moniker = Moniker,
                Data = new[]
                {
                    new Scalar(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), Price1),
                    new Scalar(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Price2)
                }
            };
        }
    }
}
