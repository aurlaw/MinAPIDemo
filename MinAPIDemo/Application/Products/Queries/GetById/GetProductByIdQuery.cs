using MediatR;
using MinAPIDemo.Models;

namespace MinAPIDemo.Application.Products.Queries.GetById
{
    public sealed record GetProductByIdQuery(Guid ProductId) : IRequest<Product?> 
    {
    }
}

