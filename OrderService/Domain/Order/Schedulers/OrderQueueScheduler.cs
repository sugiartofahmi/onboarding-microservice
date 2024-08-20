using DotNetService.Constants.Event;
using DotNetService.Domain.Order.Requests;
using DotNetService.Domain.Order.Services;
using DotNetService.Http.API.Version1.Responses;
using DotNetService.Infrastructure.Integrations.NATs;
using DotNetService.Infrastructure.Queues;
using DotNetService.Infrastructure.Shareds;

namespace DotNetService.Domain.Order.Schedulers
{
    public class OrderQueueScheduler : BackgroundService
    {
        private readonly ILogger<OrderQueueScheduler> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly BackgroundTaskQueue _taskQueue;

        private readonly TimeSpan _interval = TimeSpan.FromMinutes(10);

        public OrderQueueScheduler(
            ILogger<OrderQueueScheduler> logger,
            IServiceProvider serviceProvider,
            BackgroundTaskQueue taskQueue
        )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _taskQueue = taskQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "DataProcessingScheduler running at: {time}",
                    DateTimeOffset.Now
                );

                await _taskQueue.QueueBackgroundWorkItemAsync(ProcessPendingData);

                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async ValueTask ProcessPendingData(CancellationToken token)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();
                    var natsIntegration =
                        scope.ServiceProvider.GetRequiredService<NATsIntegration>();
                    var pendingOrders = orderService.GetAllHasStatusPending();

                    foreach (var order in pendingOrders)
                    {
                        if (token.IsCancellationRequested)
                            break;

                        string subject = natsIntegration.Subject(
                            NATsEventModuleEnum.PRODUCT,
                            NATsEventActionEnum.GET_BY_ID,
                            NATsEventStatusEnum.REQUEST
                        );
                        var result = await natsIntegration.PublishAndGetReply<
                            string,
                            NatsResponse<ApiResponseData>
                        >(subject, Utils.JsonSerialize(new { id = order.ProductId }));
                        Models.Product product = (Models.Product)(result?.result?.Data);

                        OrderUpdateRequest updateOrder = new OrderUpdateRequest
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            ProductId = order.ProductId,
                            Quantity = order.Quantity,
                        };

                        if (product != null && product?.Stock > 0)
                        {
                            updateOrder.Status = Models.OrderStatusEnum.Accepted;
                        }
                        else
                        {
                            updateOrder.Status = Models.OrderStatusEnum.Rejected;
                        }

                        orderService.Update(updateOrder);
                    }
                }

                // _logger.LogInformation(
                //     "Finished processing pending data at: {time}",
                //     DateTimeOffset.Now
                // );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing pending data");
            }
        }
    }
}
