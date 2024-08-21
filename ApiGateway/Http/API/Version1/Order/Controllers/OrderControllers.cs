using System.Net;
using DotNetService.Domain.Order.Services;
using DotNetService.Http.API.Version1.Order.Requests;
using DotNetService.Infrastructure.Shareds;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Order.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderControllers(
        OrderService orderService,
        IHttpContextAccessor httpContextAccessor
    ) : ControllerBase
    {
        private readonly OrderService _orderService = orderService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]
        public async Task<ApiResponsePagination> Index(Query request)
        {
            return await _orderService.Index(request);
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
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("id")?.Value;
            Console.WriteLine("userId:");
            Console.WriteLine(userId);
            request.UserId = new Guid(userId);
            await _orderService.Create(request);
            return new ApiResponseData(HttpStatusCode.OK, null);
        }
    }
}
