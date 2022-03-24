using MediatR;
using MinAPIDemo.Models;

namespace MinAPIDemo.Application.Products.Commands.Update
{
    public sealed record UpdateProductCommand(Guid Id, Product Product) : IRequest
    {
        
    }
}