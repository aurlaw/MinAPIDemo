using MinAPIDemo.Models;

namespace MinAPIDemo.Repositories
{
    public interface IProductRespository
    {
         Task<IEnumerable<Product>> GetAllAsync();
         Task CreateAsync(Product product);
    }
}