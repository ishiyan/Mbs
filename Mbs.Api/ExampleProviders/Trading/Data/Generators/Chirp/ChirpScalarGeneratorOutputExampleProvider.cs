using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Chirp;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Chirp
{
    /// <inheritdoc />
    internal class ChirpScalarGeneratorOutputExampleProvider : IExamplesProvider
    {
        internal const string Name = ChirpScalarGenerator.WaveformName;
        internal const string Moniker = "100∙chirp(128 ➜ 16, 128, linear period) + 10 + noise(ρn=0.01)";

        internal const double Price1 = 209.48;
        internal const double Price2 = 210.63;

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
