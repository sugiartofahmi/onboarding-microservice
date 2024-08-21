using DotNetService.Constants.Logger;
using DotNetService.Domain.Inventory.Requests;
using DotNetService.Domain.Inventory.Services;
using DotNetService.Domain.Logging.Services;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Inventory.Listeners
{
    public class ProductUpdateListener(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        ProductService productService
    ) : ISubscriptionAction<IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;
        private readonly ProductService _productService = productService;

        public void Handle(IDictionary<string, object> data)
        {
            var jsonData = Utils.JsonSerialize(data);
            ProductUpdateRequest orderCreateRequest = new ProductUpdateRequest
            {
                Id = new Guid(data["id"].ToString()),
                Name = data["name"].ToString(),
                Description = data["description"].ToString(),
                Price = Convert.ToInt32(data["price"]),
                Stock = Convert.ToInt32(data["stock"]),
            };
            _productService.Update(orderCreateRequest.Id, orderCreateRequest);

            _logger.LogInformation(jsonData);
        }
    }
}
