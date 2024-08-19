using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetService.Constants.Logger;
using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Order.Requests;
using DotNetService.Domain.Order.Services;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Order.Listeners
{
    public class OrderDetailListen(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        OrderService orderService
    ) : IReplyAction<IDictionary<string, object>, IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;

        public readonly OrderService _orderService = orderService;

        public async Task<IDictionary<string, object>> Reply(IDictionary<string, object> data)
        {
            // EXAMPLE: Do operation for reply event
            var request = Utils.JsonDeserialize<OrderGetDetail>(Utils.JsonSerialize(data));
            var response = _orderService.DetailById(request.Id);
            var reply = new Dictionary<string, object> { { "data", response }, };

            return reply;
        }
    }
}
