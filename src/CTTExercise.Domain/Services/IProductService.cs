namespace CTTExercise.Domain.Services
{
    using CTTExercise.Domain.Entities;
    using System;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<Product> CreateProductAsync(Product entity, CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
