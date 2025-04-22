namespace CTTExercise.Tests.Persistence.Repositories
{
    using AutoFixture;
    using CTTExercise.Domain.Entities;
    using CTTExercise.Domain.Repositories;
    using CTTExercise.Persistence.Repositories;
    using CTTExercise.Persistence.Setup;
    using CTTExercise.Tests;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using MongoDB.Driver;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductRepositoryTests : TestBase
    {
        private readonly Mock<IMongoCollection<Product>> _mockCollection;
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            var databaseSettings = Fixture.Create<DatabaseSettings>();
            var _mockMongoClient = new Mock<IMongoClient>();
            var _mockDatabase = new Mock<IMongoDatabase>();
            var _loggerMock = new Mock<ILogger<ProductRepository>>();

            _mockCollection = new Mock<IMongoCollection<Product>>();

            _mockMongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
                .Returns(_mockDatabase.Object);

            _mockDatabase.Setup(x => x.GetCollection<Product>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                         .Returns(_mockCollection.Object);

            _repository = new ProductRepository(databaseSettings, _mockMongoClient.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldInsertEntity()
        {
            // Arrange
            var expectedEntity = Fixture.Create<Product>();

            _mockCollection.Setup(x => x.InsertOneAsync(expectedEntity, It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _repository.CreateProductAsync(expectedEntity, CancellationToken.None);

            // Assert
            expectedEntity.Should().BeEquivalentTo(result);

            _mockCollection.Verify(
                x => x.InsertOneAsync(
                    It.Is<Product>(e => e == expectedEntity),
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            // Arrange
            var expectedEntity = Fixture.Create<Product>();
            var expectedResult = new List<Product> { expectedEntity };

            var mockCursor = MockCursor(expectedResult);

            _mockCollection
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Product>>(),
                                        It.IsAny<FindOptions<Product>>(),
                                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _repository.GetProductByIdAsync(expectedEntity.Id, CancellationToken.None);

            // Assert
            expectedEntity.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenEntityDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            var expectedResult = Enumerable.Empty<Product>();

            var mockCursor = MockCursor(expectedResult);

            _mockCollection
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Product>>(),
                                        It.IsAny<FindOptions<Product>>(),
                                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _repository.GetProductByIdAsync(id, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

        private static Mock<IAsyncCursor<T>> MockCursor<T>(IEnumerable<T> expectedValues)
        {
            var _mockCursor = new Mock<IAsyncCursor<T>>();

            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                             .Returns(true)
                             .Returns(false);

            _mockCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true)
                       .ReturnsAsync(false);

            _mockCursor.Setup(x => x.Current)
                       .Returns(expectedValues);

            return _mockCursor;
        }
    }
}
