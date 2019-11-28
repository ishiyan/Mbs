using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.RepetitiveSample;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    /// <inheritdoc />
    internal class RepetitiveSampleScalarGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Scalar>>
    {
        internal const string Name = RepetitiveSampleScalarGenerator.WaveformName;
        internal const string Moniker = "repetitive scalar sample (len=252)";

        internal const double Price1 = 91.50;
        internal const double Price2 = 94.81;

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
                    new Scalar(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Price2)
                }
            };
        }
    }
}
