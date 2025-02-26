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
    public class DataControllerTests
    {
        private readonly Mock<ILogger<DataController>> _logger;
        private readonly Mock<IMediator> _mockMediator;
        private readonly DataController _controller;

        public DataControllerTests()
        {
            _logger = new Mock<ILogger<DataController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new DataController(_logger.Object, _mockMediator.Object);
        }

        [Test]
        public async Task MemberData_ShouldReturnOkResult_WhenMediatorReturnsData()
        {
            var memberData = new List<MemberDTO> { new MemberDTO { Id = 1, Name = "John", Surname = "Dive", BookingCount = 2, DateJoined = DateTime.Now } };
            _mockMediator.Setup(m => m.Send(It.IsAny<MemberQueries>(), default))
                .ReturnsAsync(memberData);

            var result = await _controller.MemberData();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(memberData, okResult.Value);
        }

        [Test]
        public async Task InventoryData_ShouldReturnOkResult_WhenMediatorReturnsData()
        {
            var inventoryData = new List<InventoryDTO> { new InventoryDTO { Id = 1, Title = "Mr", Description = "Test", ExpirationDate = DateTime.Now, RemainingCount = 9 } };
            _mockMediator.Setup(m => m.Send(It.IsAny<InventoryQueries>(), default))
                .ReturnsAsync(inventoryData);

            var result = await _controller.InventoryData();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(inventoryData, okResult.Value);
        }

        [Test]
        public async Task MemberData_ShouldCallMediatorOnce()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<MemberQueries>(), default))
                .ReturnsAsync(new List<MemberDTO> { });

            await _controller.MemberData();

            _mockMediator.Verify(m => m.Send(It.IsAny<MemberQueries>(), default), Times.Once);
        }

        [Test]
        public async Task InventoryData_ShouldCallMediatorOnce()
        {
            _mockMediator.Setup(m => m.Send(It.IsAny<InventoryQueries>(), default))
                .ReturnsAsync(new List<InventoryDTO> { });

            await _controller.InventoryData();

            _mockMediator.Verify(m => m.Send(It.IsAny<InventoryQueries>(), default), Times.Once);
        }
    }
}
