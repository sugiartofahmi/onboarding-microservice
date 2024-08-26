using DotNetService.Domain.Inventory.Repositories;
using DotNetService.Domain.Inventory.Requests;
using DotNetService.Http.API.Version1;

namespace DotNetService.Domain.Inventory.Services
{
    public class ProductService(
        ProductQueryRepository productQueryRepository,
        ProductStoreRepository productStoreRepository
    )
    {
        private readonly ProductQueryRepository _productQueryRepository = productQueryRepository;
        private readonly ProductStoreRepository _productStoreRepository = productStoreRepository;

        public async Task<PaginationModel> Index(ProductQueryRequest query = null)
        {
            var data = await _productQueryRepository.Pagination(query);
            int count = await _productQueryRepository.Count(query);
            decimal pageInCount = ((decimal)count) / query.PerPage;
            PaginationModel paginate =
                new()
                {
                    TotalPage = (int)Math.Ceiling(pageInCount),
                    Page = query.Page,
                    PerPage = query.PerPage,
                    Data = data,
                    Total = count
                };

            return paginate;
        }

        public async Task<Models.Product> Create(ProductCreateRequest request)
        {
            var data = new Models.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            };
            var user = await _productStoreRepository.Create(data);

            return await this.Detail(user.Id);
        }

        public async Task<Models.Product> Detail(Guid id)
        {
            return await _productQueryRepository.FindOneById(id, false);
        }

        public async Task<Models.Product> Update(Guid id, ProductUpdateRequest dataUpdate)
        {
            var data = new Models.Product
            {
                Id = id,
                Name = dataUpdate.Name,
                Description = dataUpdate.Description,
                Price = dataUpdate.Price,
                Stock = dataUpdate.Stock
            };
            var updatedData = await _productStoreRepository.Update(id, data);

            return await this.Detail(updatedData.Id);
        }

        public async Task Delete(Guid id)
        {
            await _productStoreRepository.Delete(id);
        }
    }
}
