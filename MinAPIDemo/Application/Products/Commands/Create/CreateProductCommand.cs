using MediatR;
using MinAPIDemo.Models;

namespace MinAPIDemo.Application.Products.Commands.Create
{
    public sealed record CreateProductCommand(Product Product) : IRequest
    {
    }
}