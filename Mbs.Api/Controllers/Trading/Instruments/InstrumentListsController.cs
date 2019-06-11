using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mbs.Api.Extensions.ExceptionHandling;
using Mbs.Api.Extensions.Swagger;
using Mbs.Api.Models.Trading.Instruments;
using Mbs.Api.Services.Trading.Instruments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mbs.Api.Controllers.Trading.Instruments
{
    /// <summary>
    /// Manages collections of <see cref="Instrument"/>.
    /// </summary>
    [ApiVersion(Constants.ApiVersionOne)]
    [Route(BaseUri)]
    [Produces(Constants.ApplicationJsonContentType)]
    [ApiExplorerSettings(GroupName = Constants.InstrumentListsGroupName)]
    public class InstrumentListsController : ControllerBase
    {
        private const string BaseUri = Constants.ApiVersionOneSegment + Constants.InstrumentListsSegment + "{listName}";
        private readonly IInstrumentListDataService instrumentList;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentListsController"/> class.
        /// </summary>
        /// <param name="instrumentList">An instance of the <see cref="IInstrumentListDataService"/>.</param>
        public InstrumentListsController(IInstrumentListDataService instrumentList)
        {
            this.instrumentList = instrumentList;
        }

        /// <summary>
        /// Gets a collection of <see cref="Instrument"/> for a given list name.
        /// </summary>
        /// <remarks>
        /// ### Simple request ###
        /// <![CDATA[api/v1/instruments/lists/euronext]]>
        /// </remarks>
        /// <param name="listName">A name of the list to get.</param>
        /// <returns>A collection of <see cref="Instrument"/>.</returns>
        /// <response code="200">Returns a collection of <see cref="Instrument"/>.</response>
        /// <response code="400">If the name is null or empty.</response>
        /// <response code="404">If list specified in the request does not exist or is empty.</response>
        /// <response code="500">If an internal server error occured.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Instrument>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InternalError))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InternalError))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalError))]
        [Produces(typeof(IEnumerable<Instrument>))]
        public async Task<IActionResult> GetListAsync([FromRoute]string listName)
        {
            if (!ModelState.IsValid)
                return BadRequest(new InternalError(ModelState));
            if (string.IsNullOrEmpty(listName))
                return BadRequest(new InternalError { StatusCode = StatusCodes.Status400BadRequest, Message = "List name is null or empty." });

            var list = (await instrumentList.GetListAsync(listName)).ToList();
            if (!list.Any())
                return NotFound(new InternalError { StatusCode = StatusCodes.Status404NotFound, Message = $"List with name {listName} does not exist or is empty." });

            return Ok(list);
        }
    }
}
