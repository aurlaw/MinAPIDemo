using Microsoft.EntityFrameworkCore;
using MinAPIDemo.Common;
using MinAPIDemo.Data;
using MinAPIDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// configurations
var sqliteConn = builder.Configuration.GetConnectionString("Sqlite");

// configure services
builder.Services.AddDbContext<EfContext>(options => 
    options.UseSqlite(sqliteConn, sql =>
        sql.MigrationsAssembly(typeof(Product).Assembly.FullName)));
builder.Services.AddAutoMapper(typeof(Product));
builder.Services.AddEndpointDefinitions(typeof(Product));

var app = builder.Build();
// configure
app.UseEndpointDefinitions();


app.MapGet("/", () => "Hello World!");

app.Run();

