using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.RepetitiveSample;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample
{
    /// <inheritdoc />
    internal class RepetitiveSampleOhlcvGeneratorOutputExampleProvider : IExamplesProvider<SyntheticDataGeneratorOutput<Ohlcv>>
    {
        internal const string Name = RepetitiveSampleOhlcvGenerator.WaveformName;
        internal const string Moniker = "repetitive ohlcv sample (len=252)";

        internal const double Open1 = 92.5;
        internal const double High1 = 93.25;
        internal const double Low1 = 90.75;
        internal const double Close1 = 91.5;
        internal const double Volume1 = 4077500;

        internal const double Open2 = 91.50;
        internal const double High2 = 94.94;
        internal const double Low2 = 91.40;
        internal const double Close2 = 94.81;
        internal const double Volume2 = 4955900;

        /// <inheritdoc />
        public SyntheticDataGeneratorOutput<Ohlcv> GetExamples()
        {
            return new SyntheticDataGeneratorOutput<Ohlcv>
            {
                Name = Name,
                Moniker = Moniker,
                Data = new[]
                {
                    new Ohlcv(DefaultParameterValues.StartDate.Add(DefaultParameterValues.SessionEndTime), Open1, High1, Low1, Close1, Volume1),
                    new Ohlcv(DefaultParameterValues.StartDate.AddDays(1).Add(DefaultParameterValues.SessionEndTime), Open2, High2, Low2,  Close2, Volume2),
                },
            };
        }
    }
}
