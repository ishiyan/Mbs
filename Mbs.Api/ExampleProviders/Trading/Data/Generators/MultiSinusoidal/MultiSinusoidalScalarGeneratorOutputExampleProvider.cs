using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.MultiSinusoidal;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.MultiSinusoidal
{
    /// <inheritdoc />
    internal class MultiSinusoidalScalarGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Scalar>>
    {
        internal const string Name = MultiSinusoidalScalarGenerator.WaveformName;
        internal const string Moniker = "10∙cos(2π∙t/16) + 10 + noise(ρn=0.01), v=100, ρb=0.2, ρs=0.3";

        internal const double Price1 = 29.94;
        internal const double Price2 = 29.39;

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
