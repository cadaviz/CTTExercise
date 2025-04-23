namespace CTTExercise.Tests.WebApi.Controllers
{
    using AutoFixture;
    using CTTExercise.Domain.Entities;
    using CTTExercise.Domain.Services;
    using CTTExercise.Tests;
    using CTTExercise.WebApi.Controllers;
    using CTTExercise.WebApi.Requests;
    using CTTExercise.WebApi.Responses;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;

    public class ProductControllerTests : TestBase
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            var loggerMock = new Mock<ILogger<ProductController>>();
            _productServiceMock = new Mock<IProductService>();
            _controller = new ProductController(loggerMock.Object, _productServiceMock.Object);
        }

        [Fact]
        public async Task RegisterProduct_ShouldReturnBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            RegisterProductRequest? request = null;

            // Act
            var result = await _controller.RegisterProduct(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            var badRequestObjectResult =  result.Should().BeOfType<BadRequestObjectResult>().Which;

            badRequestObjectResult.Should().NotBeNull();
            badRequestObjectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnCreated_WhenRequestIsValid()
        {
            // Arrange
            var request = Fixture.Create<RegisterProductRequest>();
            var product = Fixture.Create<Product>();

            _productServiceMock
                .Setup(s => s.CreateProductAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.RegisterProduct(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Which;
            createdAtActionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            createdAtActionResult.Value.Should().NotBeNull();
            var registerProductResponse = createdAtActionResult.Value.Should().BeOfType<RegisterProductResponse>().Which;
            createdAtActionResult.RouteValues.Should().NotBeNullOrEmpty();
            createdAtActionResult.RouteValues.Should().ContainKey("id");

            var idValue = createdAtActionResult.RouteValues!.GetValueOrDefault("id")!;
            idValue.Should().NotBeNull();
            idValue.Should().NotBe(Guid.Empty);
            idValue.Should().BeOfType<Guid>();
            idValue.Should().Be(registerProductResponse.Id);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnOk_WhenProductIsFound()
        {
            // Arrange
            var expectedProduct = Fixture.Create<Product>();
            var productId = expectedProduct.Id;

            _productServiceMock
              .Setup(s => s.GetProductByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(expectedProduct);

            // Act
            var result = await _controller.GetProductById(productId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            var okResult = result.Should().BeOfType<OkObjectResult>().Which;

            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().NotBeNull();
            okResult.Value.Should().BeOfType<GetProductResponse>();
        }

        [Fact]
        public async Task GetProductById_ShouldReturnNotFound_WhenProductIsNotFound()
        {
            // Arrange
            Product? expectedProduct = null;
            var productId = Guid.NewGuid();

            _productServiceMock
              .Setup(s => s.GetProductByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(expectedProduct);

            // Act
            var result = await _controller.GetProductById(productId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Which;

            notFoundResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            notFoundResult.Value.Should().NotBeNull();

            var problemDetails = notFoundResult.Value.Should().BeOfType<ProblemDetails>().Which;
            problemDetails.Status.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
