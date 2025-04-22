namespace CTTExercise.Persistence.Repositories
{
    using CTTExercise.Domain.Entities;
    using CTTExercise.Domain.Repositories;
    using CTTExercise.Persistence.Setup;
    using CTTExercise.Shared.Extensions;
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

        public async Task<Product> CreateProductAsync(Product entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);

            _logger.LogDebugIfEnabled("Entity was inserted. Entity='{Entity}'", entity);

            return entity;
        }

        public Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
#pragma warning disable S1905
            return _collection.Find(filter => filter.Id == id)
                              .SingleOrDefaultAsync(cancellationToken) as Task<Product?>;
#pragma warning restore S1905
        }
    }
}
