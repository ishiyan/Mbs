using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Mbs.Api.Controllers.Trading.Data.Historical;
using Mbs.Api.Extensions.ExceptionHandling;
using Mbs.Api.Mappers.Trading.Data.Historical;
using Mbs.Api.Models.Trading.Data.Historical;
using Mbs.Api.Services.Trading.Data.Historical;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Instruments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Mbs.Api.UnitTests.Controllers.Trading.Data.Historical
{
    [TestClass]
    public class EuronextControllerTests
    {
        private const string TimeoutMessage = "timeout message";
        private const string ExceptionMessage = "exception message";
        private static readonly FieldInfo IsDataAdjustedFieldInfo = typeof(HistoricalDataRequest)
            .GetTypeInfo()
            .DeclaredFields
            .Single(info => info.Name.Equals("<IsDataAdjusted>k__BackingField", StringComparison.Ordinal));

        private readonly IMapper mapper = CreateMapper();
        private readonly Mock<IEuronextHistoricalDataService> mockService = new Mock<IEuronextHistoricalDataService>();
        private readonly HistoricalDataRequest validRequest = new HistoricalDataRequest();
        private readonly HistoricalDataRequest notFoundRequest = new HistoricalDataRequest();
        private readonly HistoricalDataRequest badRequest = new HistoricalDataRequest();
        private readonly HistoricalDataRequest timeoutRequest = new HistoricalDataRequest();
        private readonly HistoricalDataRequest exceptionRequest = new HistoricalDataRequest();

        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly List<Ohlcv> emptyList = new List<Ohlcv>();
        private readonly List<Ohlcv> fetchedList = new List<Ohlcv>
        {
            new Ohlcv(new DateTime(2009, 1, 1), 3, 4, 1, 2, 1000),
            new Ohlcv(new DateTime(2009, 1, 2), 6, 8, 2, 4, 2000),
        };

        private EuronextController controller;

        [TestInitialize]
        public void Initialize()
        {
            badRequest.Instrument = new Instrument();
            badRequest.StartDate = new DateTime(2010, 1, 1);
            badRequest.EndDate = new DateTime(2009, 1, 1);

            mockService
                .Setup(x => x.FetchOhlcvAsync(It.Is<HistoricalDataRequest>(req => req == validRequest)))
                .ReturnsAsync((HistoricalDataRequest req) =>
                {
                    SetIsDataAdjusted(req, req.AdjustedDataIfPresent);
                    return fetchedList;
                });
            mockService
                .Setup(x => x.FetchOhlcvAsync(It.Is<HistoricalDataRequest>(req => req == notFoundRequest)))
                .ReturnsAsync((HistoricalDataRequest req) =>
                {
                    SetIsDataAdjusted(req, req.AdjustedDataIfPresent);
                    return emptyList;
                });
            mockService
                .Setup(x => x.FetchOhlcvAsync(It.Is<HistoricalDataRequest>(req => req == timeoutRequest)))
                .ThrowsAsync(new TimeoutException(TimeoutMessage));
            mockService
                .Setup(x => x.FetchOhlcvAsync(It.Is<HistoricalDataRequest>(req => req == exceptionRequest)))
                .ThrowsAsync(new Exception(ExceptionMessage));

            controller = new EuronextController(mapper, mockService.Object);
        }

        [TestMethod]
        public async Task GetOhlcvAsync_ValidRequest_ListFetched()
        {
            var actionResult = await controller.GetOhlcvAsync(validRequest).ConfigureAwait(false);

            Assert.IsNotNull(
                actionResult,
                "actionResult should not be null");
            Assert.IsInstanceOfType(
                actionResult,
                typeof(OkObjectResult),
                "actionResult should be of type OkObjectResult");

            var objectResult = (OkObjectResult)actionResult;
            Assert.IsTrue(
                objectResult.StatusCode.HasValue,
                "objectResult should have status code");
            Assert.AreEqual(
                StatusCodes.Status200OK,
                objectResult.StatusCode.Value,
                "objectResult status code should be 200");
            Assert.IsInstanceOfType(
                objectResult.Value,
                typeof(HistoricalDataReply<Ohlcv>),
                "objectResult.Value should be of type HistoricalDataReply<Ohlcv>");

            var result = (HistoricalDataReply<Ohlcv>)objectResult.Value;
            Assert.IsTrue(
                result.IsDataAdjusted.HasValue,
                "result.IsDataAdjusted should have value");
            Assert.AreEqual(
                validRequest.AdjustedDataIfPresent,
                result.IsDataAdjusted.Value,
                "result.IsDataAdjusted should have correct value");

            var list = (List<Ohlcv>)result.Data;
            Assert.AreEqual(
                fetchedList.Count,
                list.Count,
                "list should have correct count");
            Assert.AreEqual(
                fetchedList[0].Typical,
                list[0].Typical,
                "list should have correct data (0)");
            Assert.AreEqual(
                fetchedList[1].Typical,
                list[1].Typical,
                "list should have correct data (1)");
        }

        [TestMethod]
        public async Task GetOhlcvAsync_ValidRequest_ServiceCalledOnce()
        {
            await controller.GetOhlcvAsync(validRequest).ConfigureAwait(false);

            mockService.Verify(
                x => x.FetchOhlcvAsync(It.IsAny<HistoricalDataRequest>()),
                Times.Once);
        }

        [TestMethod]
        public async Task GetOhlcvAsync_NotFound_ErrorReturned()
        {
            var actionResult = await controller.GetOhlcvAsync(notFoundRequest).ConfigureAwait(false);

            Assert.IsNotNull(
                actionResult,
                "actionResult should not be null");
            Assert.IsInstanceOfType(
                actionResult,
                typeof(NotFoundObjectResult),
                "actionResult should be of type NotFoundObjectResult");

            var objectResult = (NotFoundObjectResult)actionResult;
            Assert.IsTrue(
                objectResult.StatusCode.HasValue,
                "objectResult should have status code");
            Assert.AreEqual(
                StatusCodes.Status404NotFound,
                objectResult.StatusCode.Value,
                "objectResult status code should be 404");
            Assert.IsInstanceOfType(
                objectResult.Value,
                typeof(InternalError),
                "objectResult.Value should be of type Error");

            var result = (InternalError)objectResult.Value;
            Assert.AreEqual(
                StatusCodes.Status404NotFound,
                result.StatusCode,
                "result.StatusCode should have correct value");
            Assert.IsTrue(
                result.Message.Contains("does not exist or is empty", StringComparison.Ordinal),
                "result.Message should have correct value");
            Assert.IsNull(result.Details, "result.Details should be null");
        }

        [TestMethod]
        public async Task GetOhlcvAsync_NotFound_ServiceCalledOnce()
        {
            await controller.GetOhlcvAsync(notFoundRequest).ConfigureAwait(false);

            mockService.Verify(
                x => x.FetchOhlcvAsync(It.IsAny<HistoricalDataRequest>()),
                Times.Once);
        }

        [TestMethod]
        public async Task GetOhlcvAsync_BadRequest_ErrorReturned()
        {
            DoValidation(badRequest);
            var actionResult = await controller.GetOhlcvAsync(badRequest).ConfigureAwait(false);
            controller.ModelState.Clear();

            Assert.IsNotNull(
                actionResult,
                "actionResult should not be null");
            Assert.IsInstanceOfType(
                actionResult,
                typeof(BadRequestObjectResult),
                "actionResult should be of type BadRequestObjectResult");

            var objectResult = (BadRequestObjectResult)actionResult;
            Assert.IsTrue(
                objectResult.StatusCode.HasValue,
                "objectResult should have status code");
            Assert.AreEqual(
                StatusCodes.Status400BadRequest,
                objectResult.StatusCode.Value,
                "objectResult status code should be 400");
            Assert.IsInstanceOfType(
                objectResult.Value,
                typeof(InternalError),
                "objectResult.Value should be of type Error");

            var result = (InternalError)objectResult.Value;
            Assert.AreEqual(
                400,
                result.StatusCode,
                "result.StatusCode should have correct value");
            Assert.IsTrue(
                result.Message.Contains("Invalid parameters", StringComparison.Ordinal),
                "result.Message should have correct value");
            Assert.IsNotNull(result.Details, "result.Details should not be null");
            Assert.AreEqual(
                1,
                result.Details.Count(),
                "result.Details should have correct count");
            Assert.IsTrue(
                result.Details
                    .ElementAt(0)
                    .Message
                    .Contains("EndDateTime should be greater than the StartDateTime", StringComparison.Ordinal),
                "result.Details should have correct value");
        }

        [TestMethod]
        public async Task GetOhlcvAsync_BadRequest_ServiceNotCalled()
        {
            DoValidation(badRequest);
            await controller.GetOhlcvAsync(badRequest).ConfigureAwait(false);
            controller.ModelState.Clear();

            mockService.Verify(
                x => x.FetchOhlcvAsync(It.IsAny<HistoricalDataRequest>()),
                Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), ExceptionMessage)]
        public async Task GetOhlcvAsync_InternalServerError_ThrowsException()
        {
            // We cannot test this correctly because the exception handling middleware is absent.
            await controller.GetOhlcvAsync(exceptionRequest).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException), TimeoutMessage)]
        public async Task GetOhlcvAsync_Timeout_ThrowsException()
        {
            // We cannot test this correctly because the exception handling middleware is absent.
            await controller.GetOhlcvAsync(timeoutRequest).ConfigureAwait(false);
        }

        private static void SetIsDataAdjusted(HistoricalDataRequest instance, bool? value)
        {
            IsDataAdjustedFieldInfo.SetValue(instance, value);
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(HistoricalDataReplyProfile)));
            return new Mapper(config);
        }

        private void DoValidation(HistoricalDataRequest request)
        {
            var validationResults = new List<ValidationResult>();
            bool validated = Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true);
            if (!validated)
            {
                controller.ModelState.AddModelError("error key", validationResults[0].ErrorMessage);
            }
        }
    }
}
