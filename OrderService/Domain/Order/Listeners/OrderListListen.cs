using DotNetService.Constants.Logger;
using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Order.Services;
using DotNetService.Http.API.Version1;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Order.Listeners
{
    public class OrderListListen(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        OrderService orderService
    ) : IReplyAction<IDictionary<string, object>, IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;

        public readonly OrderService _orderService = orderService;

        public IDictionary<string, object> Reply(IDictionary<string, object> data)
        {
            var request = Utils.JsonDeserialize<Query>(Utils.JsonSerialize(data));
            var response = _orderService.Index(request);

            return new Dictionary<string, object>
            {
                { "TotalPage", response.TotalPage },
                { "Total", response.Total },
                { "Page", response.Page },
                { "PerPage", response.PerPage },
                { "Data", response.Data }
            };
        }
    }
}
