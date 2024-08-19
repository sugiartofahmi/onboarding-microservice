using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotNetService.Constants.Event;
using DotNetService.Domain.Order.Services;
using DotNetService.Http.API.Version1.Order.Requests;
using DotNetService.Infrastructure.Integrations.NATs;
using DotNetService.Infrastructure.Shareds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Order.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderControllers(OrderService orderService) : ControllerBase
    {
        private readonly OrderService _orderService = orderService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResponsePagination> Index(Query request)
        {
            return await _orderService.Index(request);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ApiResponse> Detail(Guid id)
        {
            var result = await _orderService.Detail(id);
            return new ApiResponseData(HttpStatusCode.OK, result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResponse> Create(OrderCreateRequest request)
        {
            await _orderService.Create(request);
            return new ApiResponseData(HttpStatusCode.OK, null);
        }
    }
}
