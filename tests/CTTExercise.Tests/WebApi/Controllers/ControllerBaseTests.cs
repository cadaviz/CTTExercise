namespace CTTExercise.Tests.WebApi.Controllers
{
    using CTTExercise.WebApi.Validators;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;

    public class ControllerBaseTests : TestBase
    {
        private readonly FakeControllerBase _controller;

        public ControllerBaseTests()
        {
            var loggerMock = new Mock<ILogger<FakeControllerBase>>();
            _controller = new FakeControllerBase(loggerMock.Object);
        }


        [Fact]
        public void BadRequest_ShouldReturnBadRequestObjectResult_WhenValidationFails()
        {
            // Arrange
            var expectedErrorMessage = "Validation error 1";
            var expectedErrorTitle = "One or more validation errors occurred.";
            var validationResult = new ValidationResult();
            validationResult.Errors.Add(expectedErrorMessage);

            var expectedValidationProblemDetails = new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = expectedErrorTitle,
                Errors = new Dictionary<string, string[]>() {
                    { "ValidationErrors", validationResult.Errors.ToArray() }
                },
            };

            // Act
            var result = _controller.BadRequest(validationResult);

            // Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Which;
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestResult.Value.Should().NotBeNull();

            var applicationErrorResponse = badRequestResult.Value.Should().BeOfType<ValidationProblemDetails>().Which;
            applicationErrorResponse.Should().BeEquivalentTo(expectedValidationProblemDetails);
        }

        [Fact]
        public void OkOrNotFound_ShouldReturnOkObjectResult_WhenResponseIsNotNull()
        {
            // Arrange
            var response = new { Name = "Foo" };

            // Act
            var result = _controller.OkOrNotFound(response);

            // Assert
            result.Should().NotBeNull();
            var okObjectResult = result.Should().BeOfType<OkObjectResult>().Which;
            okObjectResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okObjectResult.Value.Should().NotBeNull();
            okObjectResult.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void OkOrNotFound_ShouldReturnNotFoundResult_WhenResponseIsNull()
        {
            // Arrange
            object? response = null;
            var expectedMessage = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Detail = "No resource found.",
            };

            // Act
            var result = _controller.OkOrNotFound(response);

            // Assert
            result.Should().NotBeNull();
            var notFoundObjectResult = result.Should().BeOfType<NotFoundObjectResult>().Which;
            notFoundObjectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            notFoundObjectResult.Value.Should().NotBeNull();

            var problemDetails = notFoundObjectResult.Value.Should().BeOfType<ProblemDetails>().Which;
            problemDetails.Should().BeEquivalentTo(expectedMessage);
        }
    }

    public class FakeControllerBase : CTTExercise.WebApi.Controllers.ControllerBase
    {
        internal FakeControllerBase(ILogger<FakeControllerBase> logger) : base(logger) { }

        internal new IActionResult OkOrNotFound(object? response, string notFoundDetail = "No resource found.") => base.OkOrNotFound(response, notFoundDetail);

        internal new IActionResult BadRequest(ValidationResult validationResult) => base.BadRequest(validationResult);
    }
}
