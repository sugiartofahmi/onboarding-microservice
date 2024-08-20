using DotNetService.Domain.Order.Services;
using DotNetService.Infrastructure.Queues;

namespace DotNetService.Domain.Order.Schedulers
{
    public class OrderQueueScheduler : BackgroundService
    {
        private readonly ILogger<OrderQueueScheduler> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly BackgroundTaskQueue _taskQueue;

        private readonly TimeSpan _interval = TimeSpan.FromSeconds(10);

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
            // _logger.LogInformation(
            //     "Processing pending data started at: {time}",
            //     DateTimeOffset.Now
            // );

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();
                    var datas = orderService.GetAllHasStatusPending();
                    foreach (var data in datas)
                    {
                        if (token.IsCancellationRequested)
                            break;
                        Console.WriteLine(data.Status);
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
