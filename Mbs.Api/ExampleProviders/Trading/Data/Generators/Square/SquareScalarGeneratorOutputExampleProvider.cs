using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Square;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.Square
{
    /// <inheritdoc />
    internal class SquareScalarGeneratorOutputExampleProvider : IExamplesProvider
    {
        internal const string Name = SquareScalarGenerator.WaveformName;
        internal const string Moniker = "100∙square(128) + 10 + noise(ρn=0.01)";

        internal const double Price1 = 109.79;
        internal const double Price2 = 110.58;

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
