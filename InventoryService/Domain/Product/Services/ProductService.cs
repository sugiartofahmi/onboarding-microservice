using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotNetService.Domain.Product.Repositories;
using DotNetService.Domain.Product.Requests;
using DotNetService.Http.API.Version1;
using DotNetService.Infrastructure.Shareds;

namespace DotNetService.Domain.Product.Services
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
            var data = _productQueryRepository.Pagination(query);
            int count = _productQueryRepository.Count(query);
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

        public Models.Product Create(ProductCreateRequest request)
        {
            var data = new Models.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
            };
            var user = _productStoreRepository.Create(data);

            return this.Detail(user.Id);
        }

        public Models.Product Detail(Guid id)
        {
            return _productQueryRepository.FindOneById(id, false);
        }

        public Models.Product Update(Guid id, ProductUpdateRequest dataUpdate)
        {
            var data = new Models.Product
            {
                Id = id,
                Name = dataUpdate.Name,
                Description = dataUpdate.Description,
                Price = dataUpdate.Price,
                Stock = dataUpdate.Stock
            };
            var updatedData = _productStoreRepository.Update(id, data);

            return this.Detail(updatedData.Id);
        }

        public void Delete(Guid id)
        {
            _productStoreRepository.Delete(id);
        }
    }
}
