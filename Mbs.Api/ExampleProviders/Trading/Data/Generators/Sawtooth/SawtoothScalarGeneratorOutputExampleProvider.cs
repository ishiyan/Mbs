using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sawtooth;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Sawtooth
{
    /// <inheritdoc />
    internal class SawtoothScalarGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Scalar>>
    {
        internal const string Name = SawtoothScalarGenerator.WaveformName;
        internal const string Moniker = "100∙sawtooth(128, linear) + 10 + noise(ρn=0.01)";

        internal const double Price1 = 9.98;
        internal const double Price2 = 10.84;

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
