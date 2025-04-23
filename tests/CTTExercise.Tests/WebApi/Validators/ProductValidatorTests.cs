namespace CTTExercise.Tests.WebApi.Validators
{
    using AutoFixture;
    using CTTExercise.WebApi.Requests;
    using CTTExercise.WebApi.Validators;
    using FluentAssertions;
    using System;
    using System.Collections.Generic;

    public class ProductValidatorTests : TestBase
    {
        [Fact]
        public void Validate_ShouldReturnError_WhenRequestIsNull()
        {
            // Arrange
            RegisterProductRequest? request = null;

            // Act
            var result = ProductValidator.Validate(request);

            // Assert
            result.Errors.Should().ContainSingle("Request cannot be null.");
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenStockIsNegative()
        {
            // Arrange
            var request = Fixture.Build<RegisterProductRequest>()
                                 .With(x => x.Stock, -1)
                                 .Create();

            // Act
            var result = request.Validate();

            // Assert
            result.Errors.Should().Contain("Stock cannot be negative.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_ShouldReturnError_WhenDescriptionIsEmpty(string? description)
        {
            // Arrange
            var request = Fixture.Build<RegisterProductRequest>()
                                 .With(x => x.Description, description)
                                 .Create();

            // Act
            var result = request.Validate();

            // Assert
            result.Errors.Should().Contain("Description cannot be empty.");
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenCategoriesIsEmpty()
        {
            // Arrange
            var request = Fixture.Build<RegisterProductRequest>()
                                 .With(x => x.Categories, [])
                                 .Create();

            // Act
            var result = request.Validate();

            // Assert
            result.Errors.Should().Contain("At least one category is required.");
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenCategoryContainsEmptyGuid()
        {
            // Arrange
            var categories = new List<Guid> { Guid.Empty, Guid.NewGuid() };
            var request = Fixture.Build<RegisterProductRequest>()
                     .With(x => x.Categories, categories)
                     .Create();

            // Act
            var result = request.Validate();

            // Assert
            result.Errors.Should().Contain("Category ID cannot be empty.");
        }

        [Fact]
        public void Validate_ShouldReturnError_WhenCategoriesAreNotUnique()
        {
            // Arrange
            var duplicatedGuid = Guid.NewGuid();
            var categories = new List<Guid> { duplicatedGuid, duplicatedGuid };
            var request = Fixture.Build<RegisterProductRequest>()
                 .With(x => x.Categories, categories)
                 .Create();

            // Act
            var result = request.Validate();

            // Assert
            result.Errors.Should().Contain("Category IDs must be unique.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_ShouldReturnError_WhenPriceIsZeroOrNegative(decimal price)
        {
            // Arrange
            var request = Fixture.Build<RegisterProductRequest>()
                .With(x => x.Price, price)
                .Create();

            // Act
            var result = request.Validate();

            // Assert
            result.Errors.Should().Contain("Price must be greater than zero.");
        }

        [Fact]
        public void Validate_ShouldReturnNoErrors_WhenRequestIsValid()
        {
            // Arrange
            var request = Fixture.Create<RegisterProductRequest>();

            // Act
            var result = request.Validate();

            // Assert
            result.Errors.Should().BeEmpty();
        }
    }
}
