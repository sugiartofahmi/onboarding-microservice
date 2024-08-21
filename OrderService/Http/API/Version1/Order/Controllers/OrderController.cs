using System.Net;
using DotNetService.Domain.Order.Requests;
using DotNetService.Domain.Order.Services;
using DotNetService.Infrastructure.Shareds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Order.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController(OrderService orderService) : ControllerBase
    {
        private readonly OrderService _orderService = orderService;

        [HttpGet]
        [AllowAnonymous]
        public ApiResponsePagination Index(Query request)
        {
            PaginationModel result = _orderService.Index(request);
            return new ApiResponsePagination(HttpStatusCode.OK, result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ApiResponse Detail(Guid id)
        {
            Models.Order data = _orderService.DetailById(id);

            return new ApiResponseData(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Create(OrderCreateRequest request)
        {
            await _orderService.Create(request);

            return new ApiResponseData(HttpStatusCode.OK, null);
        }
    }
}
