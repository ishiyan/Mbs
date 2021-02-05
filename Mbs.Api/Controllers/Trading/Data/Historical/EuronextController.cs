using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mbs.Api.Extensions.ExceptionHandling;
using Mbs.Api.Extensions.Swagger;
using Mbs.Api.Mappers.Trading.Data.Historical;
using Mbs.Api.Models.Trading.Data.Historical;
using Mbs.Api.Services.Trading.Data.Historical;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Historical;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mbs.Api.Controllers.Trading.Data.Historical
{
    /// <summary>
    /// Fetches an <see cref="Ohlcv"/> historical time series from the Euronext website.
    /// </summary>
    [ApiVersion(Constants.ApiVersionOne)]
    [Route(BaseUri)]
    [Produces(Constants.ApplicationJsonContentType)]
    [ApiExplorerSettings(GroupName = Constants.OnlineHistoricalDataGroupName)]
    public class EuronextController : ControllerBase
    {
        private const string BaseUri = Constants.ApiVersionOneSegment + Constants.OnlineHistoricalDataSegment + "euronext";
        private readonly IMapper mapper;
        private readonly IEuronextHistoricalDataService euronextHistoricalData;

        /// <summary>
        /// Initializes a new instance of the <see cref="EuronextController"/> class.
        /// </summary>
        /// <param name="mapper">An instance of the <see cref="IMapper"/>.</param>
        /// <param name="euronextHistoricalData">An instance of the <see cref="IEuronextHistoricalDataService"/>.</param>
        public EuronextController(IMapper mapper, IEuronextHistoricalDataService euronextHistoricalData)
        {
            this.mapper = mapper;
            this.euronextHistoricalData = euronextHistoricalData;
        }

        /// <summary>
        /// Fetches an <see cref="Ohlcv"/> historical time series from the Euronext website.
        /// </summary>
        /// <remarks>
        /// ### Required parameters ###
        /// The minimal set of parameters will fetch all the adjusted daily data for a given ISIN/MIC/type triple. Refer to Euronext website to get information about instruments.
        /// https://www.euronext.com/en/indices/directory
        /// https://www.euronext.com/en/equities/directory
        /// https://www.euronext.com/en/etf/directory
        ///
        /// 1. **instrument.SecurityId**:        *FR0010533075*
        /// 2. **instrument.SecurityIdSource**:  *isin* (should always be isin)
        /// 3. **instrument.Type**:              *stock*
        /// 4. **instrument.Exchange.Mic**:      *xpar*
        ///
        /// <![CDATA[api/v1/data/historical/online/euronext/ohlcvs?instrument.SecurityId=FR0010533075&instrument.SecurityIdSource=isin&instrument.Type=stock&instrument.Exchange.Mic=xpar]]>
        /// ### Optional parameters ###
        /// Specify the date range, the time granularity, the trading session closing time and if data should be adjusted.
        /// 5. **adjustedDataIfPresent**: *false*
        /// 6. **timeGranularity**:       *week1*
        /// 7. **endofdayClosingTime**:   *17:30:00*
        /// 8. **startDate**:             *2009-01-01*
        /// 9. **endDate**:               *2010-12-31*.
        ///
        /// <![CDATA[api/v1/data/historical/online/euronext/ohlcvs?instrument.SecurityId=FR0010533075&instrument.SecurityIdSource=isin&instrument.Type=stock&instrument.Exchange.Mic=xpar&adjustedDataIfPresent=false&timeGranularity=week1&endofdayClosingTime=17:30:00&startDate=2009-01-01&endDate=2010-12-31]]>
        /// </remarks>
        /// <param name="historicalDataRequest">The parameters to generate data.</param>
        /// <returns>A fetched <see cref="Ohlcv"/> time series.</returns>
        /// <response code="200">Returns a fetched <see cref="Ohlcv"/> time series.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="404">If data specified in the request does not exist or is empty.</response>
        /// <response code="500">If an internal server error occurred.</response>
        [HttpGet(Constants.OhlcvSegment)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HistoricalDataReply<Ohlcv>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InternalError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InternalError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalError))]
        [Produces(typeof(HistoricalDataReply<Ohlcv>))]
        public async Task<IActionResult> GetOhlcvAsync([FromQuery]HistoricalDataRequest historicalDataRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new InternalError(ModelState));
            }

            var fetched = (await euronextHistoricalData.FetchOhlcvAsync(historicalDataRequest).ConfigureAwait(false)).ToList();
            if (!fetched.Any())
            {
                return NotFound(new InternalError { StatusCode = StatusCodes.Status404NotFound, Message = "Data does not exist or is empty." });
            }

            return Ok(HistoricalDataReplyProfile.Map(mapper, historicalDataRequest, fetched));
        }
    }
}
