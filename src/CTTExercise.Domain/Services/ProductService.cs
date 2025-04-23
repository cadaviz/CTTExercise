namespace CTTExercise.Domain.Services
{
    using CTTExercise.Domain.Entities;
    using CTTExercise.Domain.Repositories;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ProductService : IProductService
    {
        private readonly ILogger<IProductService> _logger;
        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public Task<Product> CreateProductAsync(Product entity, CancellationToken cancellationToken)
        {
            return _productRepository.CreateProductAsync(entity, cancellationToken);
        }

        public Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using (_logger.BeginScope("ProductId: {ProductId}", id))
            {
                return _productRepository.GetProductByIdAsync(id, cancellationToken);
            }
        }
    }
}
