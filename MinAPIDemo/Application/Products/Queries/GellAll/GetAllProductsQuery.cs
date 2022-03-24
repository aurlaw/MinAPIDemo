using MediatR;
using MinAPIDemo.Models;

namespace MinAPIDemo.Application.Products.Queries.GellAll
{
    public sealed record GetAllProductsQuery() : IRequest<IEnumerable<Product>> 
    {

    }
}