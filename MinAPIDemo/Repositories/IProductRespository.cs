using MinAPIDemo.Models.Domain;

namespace MinAPIDemo.Repositories
{
    public interface IProductRespository
    {
         Task<IEnumerable<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default);
         Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
         Task CreateAsync(ProductEntity? product, CancellationToken cancellationToken = default);
         Task UpdateAsync(ProductEntity? product, CancellationToken cancellationToken = default);
         Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}