using AutoMapper;
using MediatR;
using MinAPIDemo.Models;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.Application.Products.Queries.GetById
{
    internal sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IMapper _mapper;
        private readonly IProductRespository respository;

        public GetProductByIdQueryHandler(IProductRespository respository, IMapper mapper)
        {
            this.respository = respository;
            _mapper = mapper;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await respository.GetByIdAsync(request.ProductId);
            return _mapper.Map<Product>(result);            
        }
    }
}