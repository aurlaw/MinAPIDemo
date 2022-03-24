using MediatR;
using MinAPIDemo.Application.Exceptions;
using MinAPIDemo.Application.Products.Commands.Create;
using MinAPIDemo.Application.Products.Commands.Delete;
using MinAPIDemo.Application.Products.Commands.Update;
using MinAPIDemo.Application.Products.Queries.GellAll;
using MinAPIDemo.Application.Products.Queries.GetById;
using MinAPIDemo.Common;
using MinAPIDemo.Models;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.EndpointDefinitions
{
    public class ProductEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            // get all
            app.MapGet("/products", async(IMediator mediator, CancellationToken cancellationToken) => {
                var query = new GetAllProductsQuery();
                await mediator.Send(query, cancellationToken);
            })
                .Produces<IEnumerable<Product>>(200)
                .WithName("GetProducts").WithTags("ProductsAPI");
            // get by id
            app.MapGet("/products/{id}", async(IMediator mediator, Guid id, CancellationToken cancellationToken) => 
            {
                var query = new GetProductByIdQuery(id);  
                var product = await mediator.Send(query, cancellationToken);
                return product is not null ? Results.Ok(product) : Results.NotFound();          
            })
                .Produces<Product>(200)
                .ProducesProblem(404)
                .WithName("GetProductById").WithTags("ProductsAPI");
            // create     
            app.MapPost("/products", async (IMediator mediator, Product product, CancellationToken cancellationToken ) =>  
            {
                var command = new CreateProductCommand(product);
                await mediator.Send(command, cancellationToken);
                // await respository.CreateAsync(product);
                return Results.Created($"/products/{product.Id}", product);

            })
                .Accepts<Product>("application/json")
                .Produces(201)
                .WithName("CreateProduct").WithTags("ProductsAPI");
            // update                
            app.MapPut("/products/{id}", async (IMediator mediator, Guid id, Product updatedProduct, ILogger<Program> logger, CancellationToken cancellationToken) => 
            {
                if(updatedProduct.Id != id)
                {
                    return Results.Conflict("Invalid Product Id");
                }
                var command = new UpdateProductCommand(id, updatedProduct);
                try 
                {
                    await mediator.Send(command, cancellationToken);
                    return Results.Ok(updatedProduct);
                }
                catch (ProductNotFoundException pe)
                {
                    return Results.NotFound(pe);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Update error");
                    return Results.StatusCode(500);
                }
                // var existing = await respository.GetByIdAsync(id);
                // if(existing is null) 
                // {
                //     return Results.NotFound();
                // }
                // await respository.UpdateAsync(updatedProduct);
                // return Results.Ok(updatedProduct);

            })
                .Accepts<Product>("application/json")
                .Produces<Product>(200)
                .ProducesValidationProblem(409)
                .ProducesProblem(404)
                .WithName("UpdateProduct").WithTags("ProductsAPI");
            // deleted    
            app.MapDelete("/products/{id}", async (IMediator mediator, Guid id, CancellationToken cancellationToken) => 
            {
                var command = new DeleteProductCommand(id);
                await mediator.Send(command, cancellationToken);
                // await respository.DeleteAsync(id);
                return Results.Ok();
            })
                .Produces(200)
                .WithName("DeleteProduct").WithTags("ProductsAPI");
        }

        public void DefineServices(IServiceCollection services)
        {
            // services.AddSingleton<IProductRespository, ProductRespository>();
        }
    }
}
