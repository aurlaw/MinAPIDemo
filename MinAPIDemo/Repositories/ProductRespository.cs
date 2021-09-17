using MinAPIDemo.Models;

namespace MinAPIDemo.Repositories
{
    public class ProductRespository : IProductRespository
    {
        public Task CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}