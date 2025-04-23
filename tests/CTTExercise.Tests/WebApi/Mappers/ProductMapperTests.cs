namespace CTTExercise.Tests.WebApi.Mappers
{
    using AutoFixture;
    using CTTExercise.Domain.Entities;
    using CTTExercise.WebApi.Requests;
    using CTTExercise.WebApi.Mappers;
    using FluentAssertions;

    public class ProductMapperTests : TestBase
    {
        [Fact]
        public void MapToDomain_ShouldMapRequestToDomain()
        {
            // Arrange
            var request = Fixture.Create<RegisterProductRequest>();

            // Act
            var result = request.MapToDomain();

            // Assert
            result.Should().BeEquivalentTo(request);
        }

        [Fact]
        public void MapToGetProductResponse_ShouldReturnNull_WhenProductIsNull()
        {
            // Arrange
            Product? product = null;

            // Act
            var result = product.MapToGetProductResponse();

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void MapToGetProductResponse_ShouldMapProductToResponse()
        {
            // Arrange
            var product = Fixture.Create<Product>();

            // Act
            var result = product.MapToGetProductResponse();

            // Assert
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void MapToRegisterProductResponse_ShouldMapProductToResponse()
        {
            // Arrange
            var product =  Fixture.Create<Product>();

            // Act
            var result = product.MapToRegisterProductResponse();

            // Assert
            result.Should().BeEquivalentTo(product);
        }
    }
}
