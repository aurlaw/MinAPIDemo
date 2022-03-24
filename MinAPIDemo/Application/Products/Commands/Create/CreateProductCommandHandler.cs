using AutoMapper;
using MediatR;
using MinAPIDemo.Models.Domain;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.Application.Products.Commands.Create
{
    internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {

        private readonly IMapper _mapper;
        private readonly IProductRespository respository;

        public CreateProductCommandHandler(IMapper mapper, IProductRespository respository)
        {
            _mapper = mapper;
            this.respository = respository;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductEntity>(request.Product);
            await respository.CreateAsync(entity, cancellationToken);

            return Unit.Value;

        }
    }
}