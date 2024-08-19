using DotNetService.Constants.Logger;
using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Order.Repositories;
using DotNetService.Domain.Order.Requests;
using DotNetService.Domain.Order.Services;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Order.Listeners
{
    public class OrderCreateListen(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        OrderService orderService
    ) : ISubscriptionAction<IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;

        public readonly OrderService _orderService = orderService;

        public void Handle(IDictionary<string, object> data)
        {
            var jsonData = Utils.JsonSerialize(data);
            OrderCreateRequest orderCreateRequest = new OrderCreateRequest
            {
                ProductId = new Guid(data["product_id"].ToString()),
                UserId = new Guid(data["user_id"].ToString()),
                Quantity = Convert.ToInt32(data["quantity"])
            };
            _orderService.Create(orderCreateRequest);

            _logger.LogInformation(jsonData);
        }
    }
}
