using MinAPIDemo.Common;
using MinAPIDemo.Models;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.EndpointDefinitions
{
    public class ProductEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            
            app.MapGet("/products", async(IProductRespository respository) => await respository.GetAllAsync())
                .WithName("GetProducts").WithTags("ProductsAPI");
            app.MapGet("/products/{id}", async(IProductRespository respository, Guid id) => 
            {
                var product = await respository.GetByIdAsync(id);    
                return product is not null ? Results.Ok(product) : Results.NotFound();          
            })
                .WithName("GetProductById").WithTags("ProductsAPI");
            app.MapPost("/products", async(IProductRespository respository, Product product ) =>  
            {
                await respository.CreateAsync(product);
                return Results.Created($"/products/{product.Id}", product);
            })
                .WithName("CreateProduct").WithTags("ProductsAPI");
            app.MapPut("/products/{id}", async(IProductRespository respository, Guid id, Product updatedProduct) => 
            {
                if(updatedProduct.Id != id)
                {
                    return Results.Conflict("Invalid Product Id");
                }
                var existing = await respository.GetByIdAsync(id);
                if(existing is null) 
                {
                    return Results.NotFound();
                }
                await respository.UpdateAsync(updatedProduct);
                return Results.Ok(updatedProduct);
            })
                .WithName("UpdateProduct").WithTags("ProductsAPI");
            app.MapDelete("/products/{id}", async(IProductRespository respository, Guid id) => 
            {
                await respository.DeleteAsync(id);
                return Results.Ok();
            })
                .WithName("DeleteProduct").WithTags("ProductsAPI");
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<IProductRespository, ProductRespository>();
        }
    }
}
