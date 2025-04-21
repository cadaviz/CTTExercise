namespace CTTExercise.Tests.Domain.Services
{
    using AutoFixture;
    using CTTExercise.Domain.Entities;
    using CTTExercise.Domain.Repositories;
    using CTTExercise.Domain.Services;
    using CTTExercise.Tests.Shared;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Moq;

    public class ProductServiceTests : TestBase
    {
        private readonly Mock<ILogger<IProductService>> _loggerMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _loggerMock = new Mock<ILogger<IProductService>>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_loggerMock.Object, _productRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldSaveProduct()
        {
            // Arrange
            var product = Fixture.Create<Product>();
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _productService.CreateProductAsync(product, cancellationToken);

            // Assert
            _productRepositoryMock.Verify(repo => repo.CreateProductAsync(product, cancellationToken), Times.Once);

            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = Fixture.Create<Product>();
            var productId = product.Id;

            _productRepositoryMock
                .Setup(repo => repo.GetProductByIdAsync(productId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(productId, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _productRepositoryMock
                .Setup(repo => repo.GetProductByIdAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _productService.GetProductByIdAsync(orderId, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
