namespace CTTExercise.Domain.Repositories
{
    using CTTExercise.Domain.Entities;
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product entity, CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
