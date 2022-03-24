using AutoMapper;
using MediatR;
using MinAPIDemo.Application.Exceptions;
using MinAPIDemo.Models.Domain;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.Application.Products.Commands.Update
{
    internal sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProductRespository respository;

        public UpdateProductCommandHandler(IMapper mapper, IProductRespository respository)
        {
            _mapper = mapper;
            this.respository = respository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductEntity>(request.Product);

            var existing = await respository.GetByIdAsync(request.Id, cancellationToken);
            if(existing is null) 
            {
                throw new ProductNotFoundException("Product Not Found", $"Product with id {request.Id} is not found");
            }
            await respository.UpdateAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}