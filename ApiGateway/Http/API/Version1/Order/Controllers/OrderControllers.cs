using System.Net;
using DotNetService.Domain.Order.Services;
using DotNetService.Http.API.Version1.Order.Requests;
using DotNetService.Infrastructure.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Order.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderControllers(OrderService orderService) : ControllerBase
    {
        private readonly OrderService _orderService = orderService;

        [HttpGet]
        public async Task<ApiResponsePagination> Index(Query request)
        {
            PaginationModel result = await _orderService.Index(request);
            return new ApiResponsePagination(HttpStatusCode.OK, result);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> Detail(Guid id)
        {
            var result = await _orderService.Detail(id);
            return new ApiResponseData(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public async Task<ApiResponse> Create(OrderCreateRequest request)
        {
            await _orderService.Create(request);
            return new ApiResponseData(HttpStatusCode.OK, null);
        }
    }
}
