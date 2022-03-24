using MediatR;

namespace MinAPIDemo.Application.Products.Commands.Delete
{
    public sealed record DeleteProductCommand(Guid ProductId): IRequest
    {        
    }
}