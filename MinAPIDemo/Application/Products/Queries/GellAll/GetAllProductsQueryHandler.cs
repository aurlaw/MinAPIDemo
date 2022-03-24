using AutoMapper;
using MediatR;
using MinAPIDemo.Models;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.Application.Products.Queries.GellAll
{
    internal sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRespository respository;

        public GetAllProductsQueryHandler(IProductRespository respository, IMapper mapper)
        {
            this.respository = respository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var results = await respository.GetAllAsync();
            if(results.Any()) 
            {
                return _mapper.Map<IEnumerable<Product>>(results);
            }
            return Enumerable.Empty<Product>();

        }
    }
}