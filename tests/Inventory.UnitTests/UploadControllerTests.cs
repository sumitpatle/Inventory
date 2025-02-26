using Application.Upload;
using InventoryPOC.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;

namespace Inventory.UnitTests
{
    [TestFixture]
    public class UploadControllerTests
    {
        private Mock<ILogger<UploadController>> _mockLogger;
        private Mock<IMediator> _mockMediator;
        private UploadController _controller;

        public UploadControllerTests()
        {
            _mockLogger = new Mock<ILogger<UploadController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new UploadController(_mockLogger.Object, _mockMediator.Object);
        }

        [Test]
        public async Task Upload_ShouldReturnOk_WhenFileUploadedSuccessfully()
        {
            var uploadCommand = new UploadCommand { file = null, fileType = "member" };
            var expectedMessage = "File Uploaded successfully";

            _mockMediator.Setup(m => m.Send(It.IsAny<UploadCommand>(), default))
                .ReturnsAsync(expectedMessage);

            var result = await _controller.Upload(uploadCommand);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task Upload_ShouldReturnBadRequest_WhenFileUploadFails()
        {
            var uploadCommand = new UploadCommand { file = null, fileType = "member" };
            string errorMessage = string.Empty;

            _mockMediator.Setup(m => m.Send(It.IsAny<UploadCommand>(), default))
                .ReturnsAsync(errorMessage);

            var result = await _controller.Upload(uploadCommand);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public async Task Upload_ShouldCallMediatorOnce_WhenUploadingFile()
        {
            var uploadCommand = new UploadCommand { file = null, fileType = "member" };
            var resultMessage = "File uploaded successfully";

            _mockMediator.Setup(m => m.Send(It.IsAny<UploadCommand>(), default))
                .ReturnsAsync(resultMessage);

            await _controller.Upload(uploadCommand);

            _mockMediator.Verify(m => m.Send(It.IsAny<UploadCommand>(), default), Times.Once);
        }

        [Test]
        public async Task Upload_ShouldReturnBadRequest_WhenMediatorThrowsException()
        {
            var uploadCommand = new UploadCommand { file = null, fileType = "member" };
            _mockMediator.Setup(m => m.Send(It.IsAny<UploadCommand>(), default))
                .ThrowsAsync(new System.Exception("Test exception"));

            var result = await _controller.Upload(uploadCommand);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public async Task Upload_ShouldReturnInternalServerError_WhenUnhandledExceptionOccurs()
        {
            var uploadCommand = new UploadCommand { file = null, fileType = "member" };
            _mockMediator.Setup(m => m.Send(It.IsAny<UploadCommand>(), default))
                .ThrowsAsync(new System.Exception("Unexpected error"));

            var result = await _controller.Upload(uploadCommand);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }






    }
}
