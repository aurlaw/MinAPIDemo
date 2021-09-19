using Microsoft.EntityFrameworkCore;
using MinAPIDemo.Models.Domain;

namespace MinAPIDemo.Data
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions options) : base(options) 
        {

        }
        public DbSet<ProductEntity> Products {get;set;} = default!;
    }
}
