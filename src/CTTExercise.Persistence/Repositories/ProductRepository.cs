namespace CTTExercise.Persistence.Repositories
{
    using CTTExercise.Domain.Entities;
    using CTTExercise.Domain.Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ProductRepository : IProductRepository
    {
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
