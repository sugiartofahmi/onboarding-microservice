using DotNetService.Constants.Logger;
using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Order.Requests;
using DotNetService.Domain.Order.Services;
using DotNetService.Http.API.Version1.Order.Responses;
using DotNetService.Infrastructure.Integrations.NATs;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;
using DotNetService.Models;

namespace DotNetService.Domain.Order.Listeners
{
    public class OrderUpdateStatusListener(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        NATsIntegration _natsIntegration,
        OrderService orderService
    ) : ISubscriptionAction<IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;

        private readonly OrderService _orderService = orderService;
        public readonly NATsIntegration _natsIntegration = _natsIntegration;

        public async void Handle(IDictionary<string, object> data)
        {
            var jsonData = Utils.JsonSerialize(data);
            var request = Utils.JsonDeserialize<OrderCheckProductResponse>(jsonData);
            var order = await _orderService.DetailById(request.OrderId);

            var updateOrder = new OrderUpdateRequest
            {
                Id = order.Id,
                Status = (OrderStatusEnum)order.Status,
                UserId = order.UserId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                UpdatedAt = DateTime.Now
            };

            if (!request.IsProductAvailable)
            {
                updateOrder.Status = OrderStatusEnum.Rejected;
            }
            else
            {
                updateOrder.Status = OrderStatusEnum.Accepted;
            }

            await _orderService.Update(updateOrder);

            _logger.LogInformation(jsonData);
        }
    }
}
