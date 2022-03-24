using AutoMapper;
using MediatR;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.Application.Products.Commands.Delete
{
    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProductRespository respository;

        public DeleteProductCommandHandler(IMapper mapper, IProductRespository respository)
        {
            _mapper = mapper;
            this.respository = respository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await respository.DeleteAsync(request.ProductId);
            return Unit.Value;
        }
    }
}