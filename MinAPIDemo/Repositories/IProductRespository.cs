using MinAPIDemo.Models;

namespace MinAPIDemo.Repositories
{
    public interface IProductRespository
    {
         Task<IEnumerable<Product>> GetAllAsync();
         Task<Product?> GetByIdAsync(Guid id);
         Task CreateAsync(Product? product);
         Task UpdateAsync(Product? product);
         Task DeleteAsync(Guid id);
    }
}