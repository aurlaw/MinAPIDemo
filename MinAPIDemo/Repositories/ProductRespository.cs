using MinAPIDemo.Models;

namespace MinAPIDemo.Repositories
{
    public class ProductRespository : IProductRespository
    {
        private readonly Dictionary<Guid, Product> _list = new();
        public Task CreateAsync(Product? product)
        {   
            if(product is not null)
            {
                _list.Add(product.Id, product);
            }
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_list.Values.AsEnumerable());
        }
        public Task<Product?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_list.GetValueOrDefault(id));
        }
        public Task UpdateAsync(Product? product)
        {
            if(product is not null)
            {
                _list[product.Id] = product;
            }        
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Guid id)
        {
            if(_list.ContainsKey(id)) 
            {
                _list.Remove(id);
            }
            return Task.CompletedTask;
        }

    }
}