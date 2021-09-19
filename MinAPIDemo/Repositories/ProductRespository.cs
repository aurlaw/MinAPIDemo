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
        
        // private readonly Dictionary<Guid, Product> _list = new();

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
                // _list.Add(product.Id, product);
            }
        }
        
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            await using var dbContext = _dbContext.CreateDbContext();
            return dbContext.Products.ProjectToQueryable<Product>().AsEnumerable();

            // return Task.FromResult(_list.Values.AsEnumerable());
        }
        
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            await using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Products.AsQueryable()
                .Where(p => p.Id == id.ToString())
                .ProjectToFirstOrDefaultAsync<Product>();
            // return Task.FromResult(_list.GetValueOrDefault(id));
        }
        
        public async Task UpdateAsync(Product? product)
        {
            if(product is not null)
            {
                var entity = _mapper.Map<ProductEntity>(product);
                await using var dbContext = _dbContext.CreateDbContext();
                var existingProduct = await dbContext.Products.AsQueryable()
                    .Where(p => p.Id == entity.Id)
                    .FirstOrDefaultAsync();
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
                .Where(p => p.Id == id.ToString())
                .FirstOrDefaultAsync();
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