namespace CTTExercise.Persistence.Repositories
{
    using CTTExercise.Domain.Entities;
    using CTTExercise.Domain.Repositories;
    using CTTExercise.Persistence.Setup;
    using Microsoft.Extensions.Logging;
    using MongoDB.Driver;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ProductRepository : IProductRepository
    {
        protected readonly IMongoCollection<Product> _collection;
        protected readonly ILogger<ProductRepository> _logger;

        protected string CollectionName => nameof(Product);

        public ProductRepository(DatabaseSettings databaseSettings, IMongoClient mongoClient, ILogger<ProductRepository> logger)
        {
            ArgumentNullException.ThrowIfNull(databaseSettings);
            ArgumentNullException.ThrowIfNull(mongoClient);
            ArgumentNullException.ThrowIfNull(CollectionName);
            ArgumentNullException.ThrowIfNull(logger);

            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            ArgumentNullException.ThrowIfNull(database);

            _collection = database.GetCollection<Product>(CollectionName);
            ArgumentNullException.ThrowIfNull(_collection);

            _logger = logger;
        }

        public Task<Product> CreateProductAsync(Product entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
