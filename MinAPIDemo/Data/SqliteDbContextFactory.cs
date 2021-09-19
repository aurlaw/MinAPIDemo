using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MinAPIDemo.Data
{
    public class SqliteDbContextFactory : IDesignTimeDbContextFactory<EfContext>
    {
        public EfContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfContext>();
            var connectionString = args.Any() ? args[0] : "Data Source=minapi.db;Cache=Shared";
            optionsBuilder.UseSqlite(connectionString, db => db
                .MigrationsAssembly(typeof(SqliteDbContextFactory).Assembly.GetName().Name));

            return new EfContext(optionsBuilder.Options);
        }
    }
}
