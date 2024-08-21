using System.Net;
using DotNetService.Domain.Order.Requests;
using DotNetService.Domain.Order.Services;
using DotNetService.Infrastructure.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Order.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController(OrderService orderService) : ControllerBase
    {
        private readonly OrderService _orderService = orderService;

        [HttpGet]
        public async Task<ApiResponsePagination> Index(Query request)
        {
            return new ApiResponsePagination(HttpStatusCode.OK, null);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> Detail(Guid id)
        {
            return new ApiResponseData(HttpStatusCode.OK, null);
        }

        [HttpPost]
        public async Task<ApiResponse> Create(OrderCreateRequest request)
        {
            return new ApiResponseData(HttpStatusCode.OK, null);
        }
    }
}
