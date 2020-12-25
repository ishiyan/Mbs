using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sinusoidal
{
    /// <inheritdoc />
    internal class SinusoidalScalarGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Scalar>>
    {
        internal const string Name = SinusoidalScalarGenerator.WaveformName;
        internal const string Moniker = "100∙cos(2π∙t/16) + 10 + noise(ρn=0.01)";

        internal const double Price1 = 209.60;
        internal const double Price2 = 203.47;

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
