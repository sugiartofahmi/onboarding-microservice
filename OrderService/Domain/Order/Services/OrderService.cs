using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotNetService.Domain.Order.Repositories;
using DotNetService.Http.API.Version1;
using DotNetService.Infrastructure.Shareds;

namespace DotNetService.Domain.Order.Services
{
    public class OrderService
    {
        private readonly OrderQueryRepository _orderQueryRepository;

        public OrderService(OrderQueryRepository orderQueryRepository)
        {
            _orderQueryRepository = orderQueryRepository;
        }

        public async Task<PaginationModel> Index(Query query = null)
        {
            var data = await _orderQueryRepository.Pagination(query);

            return new PaginationModel
            {
                Data = data,
                TotalPage = 0,
                Page = 1,
                PerPage = 1,
                Total = 1
            };
        }
    }
}
