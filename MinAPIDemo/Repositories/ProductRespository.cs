using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinAPIDemo.Data;
using MinAPIDemo.Models;
using MinAPIDemo.Models.Domain;

namespace MinAPIDemo.Repositories
{
    public class ProductRespository : IProductRespository
    {

        private readonly IDbContextFactory<EfContext> _dbContext;
        private readonly IMapper _mapper;
        
        public ProductRespository(IDbContextFactory<EfContext> dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateAsync(Product? product)
        {   
            if(product is not null)
            {
                var entity = _mapper.Map<ProductEntity>(product);
                await using var dbContext = _dbContext.CreateDbContext();
                await dbContext.Products.AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
        }
        
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            await using var dbContext = _dbContext.CreateDbContext();
            var results = dbContext.Products.AsEnumerable();
            if(results.Any()) 
            {
                return _mapper.Map<IEnumerable<Product>>(results);
            }
            return Enumerable.Empty<Product>();
        }
        
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            await using var dbContext = _dbContext.CreateDbContext();
            var result = await dbContext.Products.AsQueryable()
                .FirstOrDefaultAsync(p => p.Id == id.ToString());
            return _mapper.Map<Product>(result);
        }
        
        public async Task UpdateAsync(Product? product)
        {
            if(product is not null)
            {
                var entity = _mapper.Map<ProductEntity>(product);
                await using var dbContext = _dbContext.CreateDbContext();
                var existingProduct = await dbContext.Products.AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == entity.Id);
                if(existingProduct is not null) 
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.Quantity = product.Quantity;    

                    dbContext.Update(existingProduct);
                    await dbContext.SaveChangesAsync();
                }
                // _list[product.Id] = product;
            }        
        }
        
        public async Task DeleteAsync(Guid id)
        {
            await using var dbContext = _dbContext.CreateDbContext();
            var existingProduct = await dbContext.Products.AsQueryable()
                .FirstOrDefaultAsync(p => p.Id == id.ToString());
            if(existingProduct is not null) 
            {
                dbContext.Remove(existingProduct);
                await dbContext.SaveChangesAsync();
            }
            // if(_list.ContainsKey(id)) 
            // {
            //     _list.Remove(id);
            // }
            // return Task.CompletedTask;
        }

    }
}