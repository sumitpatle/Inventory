using Application.Booking;
using Domain.Entities;
using InventoryPOC.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Inventory.UnitTests
{
    [TestFixture]
    public class BookingControllerTests
    {
        private readonly Mock<ILogger<BookingController>> _logger;
        private readonly Mock<IMediator> _mockMediator;
        private readonly BookingController _controller;

        public BookingControllerTests()
        {
            _logger = new Mock<ILogger<BookingController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new BookingController(_logger.Object, _mockMediator.Object);
        }

        [Test]
        public async Task BookItem_ShouldReturnOkResult_WhenCommandIsValid()
        {
            var command = new BookItemCommand();
            var expectedResult = "Success";

            _mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(expectedResult);

            var result = await _controller.BookItem(command);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedResult, okResult.Value);

            // Verify mediator was called once
            _mockMediator.Verify(m => m.Send(command, default), Times.Once);
        }

        [Test]
        public async Task CancelBooking_ShouldReturnOkResult_WhenCommandIsValid()
        {
            var command = new CancelBookingCommand();
            var expectedResult = "Success";

            _mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CancelBooking(command);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedResult, okResult.Value);

            _mockMediator.Verify(m => m.Send(command, default), Times.Once);
        }

        [Test]
        public async Task Bookings_ShouldReturnOkResult_WhenQueryIsValid()
        {
            var query = new BookingQueries();
            var expectedResult = new List<BookingsDTO> { new BookingsDTO { Id = 1, InventoryItemId = 1, MemberId = 1, BookingDate = DateTime.Now } };

            _mockMediator.Setup(m => m.Send(query, default)).ReturnsAsync(expectedResult);

            var result = await _controller.Bookings(query);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedResult, okResult.Value);

            _mockMediator.Verify(m => m.Send(query, default), Times.Once);
        }
    }
}
