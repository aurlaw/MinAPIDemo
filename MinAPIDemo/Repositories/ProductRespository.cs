using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinAPIDemo.Data;
using MinAPIDemo.Models.Domain;

namespace MinAPIDemo.Repositories
{
    public class ProductRespository : IProductRespository
    {

        private readonly IDbContextFactory<EfContext> _dbContext;
        
        public ProductRespository(IDbContextFactory<EfContext> dbContext)
        {
            _dbContext = dbContext;
            // _mapper = mapper;
        }

        public async Task CreateAsync(ProductEntity? product, CancellationToken cancellationToken = default)
        {   
            if(product is not null)
            {
                // var entity = _mapper.Map<ProductEntity>(product);
                await using var dbContext = _dbContext.CreateDbContext();
                await dbContext.Products.AddAsync(product, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        
        public async Task<IEnumerable<ProductEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Products.ToListAsync(cancellationToken);
            // if(results.Any()) 
            // {
            //     return _mapper.Map<IEnumerable<Product>>(results);
            // }
            // return Enumerable.Empty<Product>();
        }
        
        public async Task<ProductEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Products.AsQueryable()
                .FirstOrDefaultAsync(p => p.Id == id.ToString(), cancellationToken);
            // return _mapper.Map<Product>(result);
        }
        
        public async Task UpdateAsync(ProductEntity? product, CancellationToken cancellationToken = default)
        {
            if(product is not null)
            {
                // var entity = _mapper.Map<ProductEntity>(product);
                await using var dbContext = _dbContext.CreateDbContext();
                var existingProduct = await dbContext.Products.AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);
                if(existingProduct is not null) 
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.Quantity = product.Quantity;    

                    dbContext.Update(existingProduct);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                // _list[product.Id] = product;
            }        
        }
        
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await using var dbContext = _dbContext.CreateDbContext();
            var existingProduct = await dbContext.Products.AsQueryable()
                .FirstOrDefaultAsync(p => p.Id == id.ToString(), cancellationToken);
            if(existingProduct is not null) 
            {
                dbContext.Remove(existingProduct);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            // if(_list.ContainsKey(id)) 
            // {
            //     _list.Remove(id);
            // }
            // return Task.CompletedTask;
        }

    }
}