using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotNetService.Constants.Event;
using DotNetService.Infrastructure.Integrations.NATs;
using DotNetService.Infrastructure.Shareds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1.Order.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderControllers(NATsIntegration natsIntegration) : ControllerBase
    {
        private readonly NATsIntegration _natsIntegration = natsIntegration;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResponse> Index([FromQuery] dynamic request)
        {
            string subject = _natsIntegration.Subject(
                NATsEventModuleEnum.ORDER,
                NATsEventActionEnum.GET,
                NATsEventStatusEnum.REQUEST
            );

            var result = await _natsIntegration.PublishAndGetReply<dynamic, dynamic>(
                subject,
                Utils.JsonSerialize(new { name = "test" })
            );
            Console.WriteLine("result:");
            Console.WriteLine(result);
            return new ApiResponseData(HttpStatusCode.OK, null);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ApiResponse> Detail(int id)
        {
            return new ApiResponseData(HttpStatusCode.OK, null);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResponse> Create([FromBody] dynamic request)
        {
            return new ApiResponseData(HttpStatusCode.OK, null);
        }
    }
}
