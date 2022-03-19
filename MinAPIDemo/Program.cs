using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinAPIDemo;
using MinAPIDemo.Common;
using MinAPIDemo.Data;
using MinAPIDemo.Models;

var builder = WebApplication.CreateBuilder(args);

// configurations
var sqliteConn = builder.Configuration.GetConnectionString("Sqlite");

// configure services
builder.Services.AddDbContextFactory<EfContext>(options => 
    options.UseSqlite(sqliteConn, sql =>
        sql.MigrationsAssembly(typeof(Product).Assembly.FullName)));
builder.Services.AddAutoMapper(typeof(Product));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });    
});
// builder.Services.AddJwtAuthService(builder.Configuration);
builder.Services.AddEndpointDefinitions(typeof(Product));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
            builder.WithOrigins("http://localhost:3000");
        });
});

var app = builder.Build();
// configure
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API V1");
});
app.UseCors();
// app.UseAuthentication();
// app.UseAuthorization();

app.UseEndpointDefinitions();
// app.MapGet("/", () => "Hello World!");

app.Run();

