namespace CTTExercise.Domain.Services
{
    using CTTExercise.Domain.Entities;
    using CTTExercise.Domain.Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
