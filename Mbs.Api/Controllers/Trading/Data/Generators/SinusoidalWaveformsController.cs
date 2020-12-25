using System.Threading.Tasks;
using Mbs.Api.ExampleProviders;
using Mbs.Api.ExampleProviders.Trading.Data.Generators;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.Sinusoidal;
using Mbs.Api.Extensions.ExceptionHandling;
using Mbs.Api.Extensions.Swagger;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.Sinusoidal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.Controllers.Trading.Data.Generators
{
    /// <summary>
    /// Generates Sinusoidal impulse waveforms.
    /// </summary>
    [ApiVersion(Constants.ApiVersionOne)]
    [Route(BaseUri)]
    [Produces(Constants.ApplicationJsonContentType)]
    [ApiExplorerSettings(GroupName = Constants.DataGenerationGroupName)]
    public class SinusoidalWaveformsController : ControllerBase
    {
        private const string BaseUri = Constants.ApiVersionOneSegment + Constants.DataGenerationSegment + "SinusoidalWaveforms";

        /// <summary>
        /// Generates a series of candlesticks which follow a sinusoidal waveform.
        /// </summary>
        /// <remarks>
        /// The sinusoidal *Ohlcv* waveform generator produces candlesticks with the following traits.
        ///
        /// 1. The mid prices of the candlesticks form a sinusoid defined by the given period, amplitude and phase.
        ///
        /// 2. The sinusoid is shifted upwards so it has minimal values at the specified minimal price level.
        ///
        /// 3. The opening and closing prices are equidistant from the mid price.
        /// The half-length of the candlestick body is defined as a ratio ∈[0, 1].
        /// When the ratio is 1, the lower price is zero and the higher price is twice the mid price.
        /// When the ratio is 0, the both opening and closing prices are equal to the the mid price.
        ///
        /// 4. The highest and the lowest prices are equidistant from the mid price.
        /// The length of the candlestick shadows is defined as a ratio ∈[0, 1].
        /// When the ratio is 1, the lowest price is zero and the highest price is twice the mid price.
        /// When the ratio is 0, the both lowest and highest prices are equal to the the mid price.
        ///
        /// 5. The volume has a constant value.
        ///
        /// 6. An optional noise may be added to mid prices.
        ///
        /// The input parameters are as follows.
        ///
        /// ### Sample count ###
        /// 1. **sampleCount**: The number of samples to generate.
        ///
        /// ### Time parameters ###
        /// 1. **sessionBeginTime**:     A start time of the trading session.
        /// 2. **sessionEndTime**:       An end time of the trading session.
        /// 3. **startDate**:            A date of the first data sample.
        /// 4. **timeGranularity**:      A time granularity of data samples.
        /// 5. **businessDayCalendar**:  An exchange holiday schedule or a general country holiday schedule.
        /// Business days do not form a continuous sequence (weekends, holidays etc.), so there is a need in differentiating between the business time and the physical time.
        ///
        /// ### Waveform parameters ###
        /// 1. **waveformSamples**:                 A number of samples in the waveform.
        /// 2. **offsetSamples**:                   A number of samples before the very first waveform sample. The value of zero means the waveform starts immediately.
        /// 3. **repetitionsCount**:                A number of repetitions of the waveform. The value of zero means infinite.
        /// 4. **noiseAmplitudeFraction**:          An amplitude of the noise as a fraction of the sample value. The noise will be not produced if the value is zero.
        /// 5. **noiseUniformRandomGeneratorKind**: A kind of an uniform random generator to produce the noise.
        /// 6. **noiseUniformRandomGeneratorSeed**: A seed of a random generator to produce the noise.
        ///
        /// ### Sinusoidal parameters ###
        /// 1. **amplitude**:    An amplitude of the sinusoid, should be positive.
        /// 2. **minimalValue**: The minimum value of a sinusoid, should be positive.
        /// 3. **period**:       A period of the sinusoid in samples, should be ≥ 2.
        /// 4. **phaseInPi**:    The phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        ///
        /// ### Ohlcv (candlestick) parameters ###
        /// 1. **candlestickShadowFraction**:  A shadow fraction, ρs, which determines the length of the candlestick shadows as a fraction of the mid price; ρs∈[0, 1].
        /// The value should be greater or equal to the candlestick body fraction.
        /// 2. **candlestickBodyFraction**:    A body fraction, ρb, which determines the half-length of the candlestick body as a fraction of the mid price; ρb∈[0, 1].
        /// The value should be less or equal to the candlestick shadow fraction.
        /// 3. **volume**:                     A volume, which is the same for all candlesticks; should be positive.
        /// </remarks>
        /// <param name="parameters">Generation parameters.</param>
        /// <returns>A generated ohlcv time series.</returns>
        /// <response code="200">If generation is successful.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost(Constants.OhlcvSegment)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SyntheticDataGeneratorOutput<Ohlcv>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InternalError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalError))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SinusoidalOhlcvGeneratorOutputExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(WaveformDataGeneratorBadRequestExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalErrorExampleProvider))]
        [Produces(typeof(SyntheticDataGeneratorOutput<Ohlcv>))]
        public async Task<IActionResult> GenerateOhlcvAsync([FromBody]SinusoidalOhlcvGeneratorParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            return Ok(await parameters.GenerateAsync(parameters.SampleCount));
        }

        /// <summary>
        /// Generates a series of quotes which follow a sinusoidal waveform.
        /// </summary>
        /// <remarks>
        /// The sinusoidal *Quote* waveform generator produces quotes with the following traits.
        ///
        /// 1. The mid prices of the quotes form a sinusoid defined by the given period, amplitude and phase.
        ///
        /// 2. The sinusoid is shifted upwards so it has minimal values at the specified minimal price level.
        ///
        /// 3. The ask and bid prices are equidistant from the mid price.
        /// The half-length of the spread is defined as a ratio ∈[0, 1].
        /// When the ratio is 1, the bid price is zero and the ask price is twice the mid price.
        /// When the ratio is 0, the both ask and bid prices are equal to the the mid price.
        ///
        /// 4. The ask and bid sizes are constant values.
        ///
        /// 5. An optional noise may be added to mid prices.
        ///
        /// The input parameters are as follows.
        ///
        /// ### Sample count ###
        /// 1. **sampleCount**: The number of samples to generate.
        ///
        /// ### Time parameters ###
        /// 1. **sessionBeginTime**:     A start time of the trading session.
        /// 2. **sessionEndTime**:       An end time of the trading session.
        /// 3. **startDate**:            A date of the first data sample.
        /// 4. **timeGranularity**:      A time granularity of data samples.
        /// 5. **businessDayCalendar**:  An exchange holiday schedule or a general country holiday schedule.
        /// Business days do not form a continuous sequence (weekends, holidays etc.), so there is a need in differentiating between the business time and the physical time.
        ///
        /// ### Waveform parameters ###
        /// 1. **waveformSamples**:                 A number of samples in the waveform.
        /// 2. **offsetSamples**:                   A number of samples before the very first waveform sample. The value of zero means the waveform starts immediately.
        /// 3. **repetitionsCount**:                A number of repetitions of the waveform. The value of zero means infinite.
        /// 4. **noiseAmplitudeFraction**:          An amplitude of the noise as a fraction of the sample value. The noise will be not produced if the value is zero.
        /// 5. **noiseUniformRandomGeneratorKind**: A kind of an uniform random generator to produce the noise.
        /// 6. **noiseUniformRandomGeneratorSeed**: A seed of a random generator to produce the noise.
        ///
        /// ### Sinusoidal parameters ###
        /// 1. **amplitude**:    An amplitude of the sinusoid, should be positive.
        /// 2. **minimalValue**: The minimum value of a sinusoid, should be positive.
        /// 3. **period**:       A period of the sinusoid in samples, should be ≥ 2.
        /// 4. **phaseInPi**:    The phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        ///
        /// ### Quote parameters ###
        /// 1. **spreadFraction**:  A spread fraction, ρs, which determines the ask and bid prices as a fraction of the mid price.
        /// 2. **askSize**:         An ask size, which is the same for all quotes; should be positive.
        /// 3. **bidSize**:         A bid size, which is the same for all quotes; should be positive.
        /// </remarks>
        /// <param name="parameters">Generation parameters.</param>
        /// <returns>A generated quote time series.</returns>
        /// <response code="200">If generation is successful.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost(Constants.QuoteSegment)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SyntheticDataGeneratorOutput<Quote>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InternalError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalError))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SinusoidalQuoteGeneratorOutputExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(WaveformDataGeneratorBadRequestExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalErrorExampleProvider))]
        [Produces(typeof(SyntheticDataGeneratorOutput<Quote>))]
        public async Task<IActionResult> GenerateQuoteAsync([FromBody]SinusoidalQuoteGeneratorParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            return Ok(await parameters.GenerateAsync(parameters.SampleCount));
        }

        /// <summary>
        /// Generates a series of trades which follow a sinusoidal waveform.
        /// </summary>
        /// <remarks>
        /// The sinusoidal *Trade* waveform generator produces trades with the following traits.
        ///
        /// 1. The prices of the trades form a sinusoid defined by the given period, amplitude and phase.
        ///
        /// 2. The sinusoid is shifted upwards so it has minimal values at the specified minimal price level.
        ///
        /// 3. The volume has a constant value.
        ///
        /// 4. An optional noise may be added to the prices.
        ///
        /// The input parameters are as follows.
        ///
        /// ### Sample count ###
        /// 1. **sampleCount**: The number of samples to generate.
        ///
        /// ### Time parameters ###
        /// 1. **sessionBeginTime**:     A start time of the trading session.
        /// 2. **sessionEndTime**:       An end time of the trading session.
        /// 3. **startDate**:            A date of the first data sample.
        /// 4. **timeGranularity**:      A time granularity of data samples.
        /// 5. **businessDayCalendar**:  An exchange holiday schedule or a general country holiday schedule.
        /// Business days do not form a continuous sequence (weekends, holidays etc.), so there is a need in differentiating between the business time and the physical time.
        ///
        /// ### Waveform parameters ###
        /// 1. **waveformSamples**:                 A number of samples in the waveform.
        /// 2. **offsetSamples**:                   A number of samples before the very first waveform sample. The value of zero means the waveform starts immediately.
        /// 3. **repetitionsCount**:                A number of repetitions of the waveform. The value of zero means infinite.
        /// 4. **noiseAmplitudeFraction**:          An amplitude of the noise as a fraction of the sample value. The noise will be not produced if the value is zero.
        /// 5. **noiseUniformRandomGeneratorKind**: A kind of an uniform random generator to produce the noise.
        /// 6. **noiseUniformRandomGeneratorSeed**: A seed of a random generator to produce the noise.
        ///
        /// ### Sinusoidal parameters ###
        /// 1. **amplitude**:    An amplitude of the sinusoid, should be positive.
        /// 2. **minimalValue**: The minimum value of a sinusoid, should be positive.
        /// 3. **period**:       A period of the sinusoid in samples, should be ≥ 2.
        /// 4. **phaseInPi**:    The phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        ///
        /// ### Trade parameters ###
        /// 1. **volume**:  A volume, which is the same for all trades; should be positive.
        /// </remarks>
        /// <param name="parameters">Generation parameters.</param>
        /// <returns>A generated trade time series.</returns>
        /// <response code="200">If generation is successful.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost(Constants.TradeSegment)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SyntheticDataGeneratorOutput<Trade>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InternalError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalError))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SinusoidalTradeGeneratorOutputExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(WaveformDataGeneratorBadRequestExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalErrorExampleProvider))]
        [Produces(typeof(SyntheticDataGeneratorOutput<Trade>))]
        public async Task<IActionResult> GenerateTradeAsync([FromBody]SinusoidalTradeGeneratorParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            return Ok(await parameters.GenerateAsync(parameters.SampleCount));
        }

        /// <summary>
        /// Generates a series of scalars which follow a sinusoidal waveform.
        /// </summary>
        /// <remarks>
        /// The sinusoidal *Scalar* waveform generator produces scalars with the following traits.
        ///
        /// 1. The values of the scalars form a sinusoid defined by the given period, amplitude and phase.
        ///
        /// 2. The sinusoid is shifted upwards so it has minimal values at the specified minimal value level.
        ///
        /// 3. An optional noise may be added to the values.
        ///
        /// The input parameters are as follows.
        ///
        /// ### Sample count ###
        /// 1. **sampleCount**: The number of samples to generate.
        ///
        /// ### Time parameters ###
        /// 1. **sessionBeginTime**:     A start time of the trading session.
        /// 2. **sessionEndTime**:       An end time of the trading session.
        /// 3. **startDate**:            A date of the first data sample.
        /// 4. **timeGranularity**:      A time granularity of data samples.
        /// 5. **businessDayCalendar**:  An exchange holiday schedule or a general country holiday schedule.
        /// Business days do not form a continuous sequence (weekends, holidays etc.), so there is a need in differentiating between the business time and the physical time.
        ///
        /// ### Waveform parameters ###
        /// 1. **waveformSamples**:                 A number of samples in the waveform.
        /// 2. **offsetSamples**:                   A number of samples before the very first waveform sample. The value of zero means the waveform starts immediately.
        /// 3. **repetitionsCount**:                A number of repetitions of the waveform. The value of zero means infinite.
        /// 4. **noiseAmplitudeFraction**:          An amplitude of the noise as a fraction of the sample value. The noise will be not produced if the value is zero.
        /// 5. **noiseUniformRandomGeneratorKind**: A kind of an uniform random generator to produce the noise.
        /// 6. **noiseUniformRandomGeneratorSeed**: A seed of a random generator to produce the noise.
        ///
        /// ### Sinusoidal parameters ###
        /// 1. **amplitude**:    An amplitude of the sinusoid, should be positive.
        /// 2. **minimalValue**: The minimum value of a sinusoid, should be positive.
        /// 3. **period**:       A period of the sinusoid in samples, should be ≥ 2.
        /// 4. **phaseInPi**:    The phase, φ, of the sinusoid in ratios of π; if φ∈[-1, 1], then the phase ∈[-π, π].
        /// </remarks>
        /// <param name="parameters">Generation parameters.</param>
        /// <returns>A generated scalar time series.</returns>
        /// <response code="200">If generation is successful.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpPost(Constants.ScalarSegment)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SyntheticDataGeneratorOutput<Scalar>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InternalError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalError))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SinusoidalScalarGeneratorOutputExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(WaveformDataGeneratorBadRequestExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalErrorExampleProvider))]
        [Produces(typeof(SyntheticDataGeneratorOutput<Scalar>))]
        public async Task<IActionResult> GenerateScalarAsync([FromBody]SinusoidalScalarGeneratorParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            return Ok(await parameters.GenerateAsync(parameters.SampleCount));
        }
    }
}
