using System.Threading.Tasks;
using Mbs.Api.ExampleProviders;
using Mbs.Api.ExampleProviders.Trading.Data.Generators;
using Mbs.Api.ExampleProviders.Trading.Data.Generators.RepetitiveSample;
using Mbs.Api.Extensions.ExceptionHandling;
using Mbs.Api.Extensions.Swagger;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Generators;
using Mbs.Trading.Data.Generators.RepetitiveSample;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Mbs.Api.Controllers.Trading.Data.Generators
{
    /// <summary>
    /// Generates RepetitiveSample impulse waveforms.
    /// </summary>
    [ApiVersion(Constants.ApiVersionOne)]
    [Route(BaseUri)]
    [Produces(Constants.ApplicationJsonContentType)]
    [ApiExplorerSettings(GroupName = Constants.DataGenerationGroupName)]
    public class RepetitiveSampleWaveformsController : ControllerBase
    {
        private const string BaseUri = Constants.ApiVersionOneSegment + Constants.DataGenerationSegment + "RepetitiveSampleWaveforms";

        /// <summary>
        /// Generates a series of candlesticks based on pre-defined samples.
        /// </summary>
        /// <remarks>
        /// The repetitive sample *Ohlcv* waveform generator produces candlesticks based on pre-defined samples.
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
        /// 1. **offsetSamples**:    A number of samples before the very first waveform sample. The value of zero means the waveform starts immediately.
        /// 2. **repetitionsCount**: A number of repetitions of the waveform. The value of zero means infinite.
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
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RepetitiveSampleOhlcvGeneratorOutputExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(WaveformDataGeneratorBadRequestExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalErrorExampleProvider))]
        [Produces(typeof(SyntheticDataGeneratorOutput<Ohlcv>))]
        public async Task<IActionResult> GenerateOhlcvAsync([FromBody]RepetitiveSampleGeneratorParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            return Ok(await parameters.GenerateOhlcvAsync(parameters.SampleCount));
        }

        /// <summary>
        /// Generates a series of quotes based on pre-defined samples.
        /// </summary>
        /// <remarks>
        /// The repetitive sample *Quote* waveform generator produces quotes based on pre-defined samples.
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
        /// 1. **offsetSamples**:    A number of samples before the very first waveform sample. The value of zero means the waveform starts immediately.
        /// 2. **repetitionsCount**: A number of repetitions of the waveform. The value of zero means infinite.
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
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RepetitiveSampleQuoteGeneratorOutputExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(WaveformDataGeneratorBadRequestExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalErrorExampleProvider))]
        [Produces(typeof(SyntheticDataGeneratorOutput<Quote>))]
        public async Task<IActionResult> GenerateQuoteAsync([FromBody]RepetitiveSampleGeneratorParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            return Ok(await parameters.GenerateQuoteAsync(parameters.SampleCount));
        }

        /// <summary>
        /// Generates a series of trades based on pre-defined samples.
        /// </summary>
        /// <remarks>
        /// The repetitive sample *Trade* waveform generator produces trades based on pre-defined samples.
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
        /// 1. **offsetSamples**:    A number of samples before the very first waveform sample. The value of zero means the waveform starts immediately.
        /// 2. **repetitionsCount**: A number of repetitions of the waveform. The value of zero means infinite.
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
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RepetitiveSampleTradeGeneratorOutputExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(WaveformDataGeneratorBadRequestExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalErrorExampleProvider))]
        [Produces(typeof(SyntheticDataGeneratorOutput<Trade>))]
        public async Task<IActionResult> GenerateTradeAsync([FromBody]RepetitiveSampleGeneratorParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            return Ok(await parameters.GenerateTradeAsync(parameters.SampleCount));
        }

        /// <summary>
        /// Generates a series of scalars based on pre-defined samples.
        /// </summary>
        /// <remarks>
        /// The repetitive sample *Scalar* waveform generator produces scalars based on pre-defined samples.
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
        /// 1. **offsetSamples**:    A number of samples before the very first waveform sample. The value of zero means the waveform starts immediately.
        /// 2. **repetitionsCount**: A number of repetitions of the waveform. The value of zero means infinite.
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
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RepetitiveSampleScalarGeneratorOutputExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(WaveformDataGeneratorBadRequestExampleProvider))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalErrorExampleProvider))]
        [Produces(typeof(SyntheticDataGeneratorOutput<Scalar>))]
        public async Task<IActionResult> GenerateScalarAsync([FromBody]RepetitiveSampleGeneratorParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            return Ok(await parameters.GenerateScalarAsync(parameters.SampleCount));
        }
    }
}
