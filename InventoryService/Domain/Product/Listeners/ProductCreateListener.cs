using DotNetService.Constants.Logger;
using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Product.Requests;
using DotNetService.Domain.Product.Services;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Product.Listeners
{
    public class ProductCreateListener(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        ProductService productService
    ) : ISubscriptionAction<IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;

        public readonly ProductService _productService = productService;

        public void Handle(IDictionary<string, object> data)
        {
            var jsonData = Utils.JsonSerialize(data);
            ProductCreateRequest orderCreateRequest = new ProductCreateRequest
            {
                Name = data["name"].ToString(),
                Description = data["description"].ToString(),
                Price = Convert.ToInt32(data["price"]),
            };
            _productService.Create(orderCreateRequest);

            _logger.LogInformation(jsonData);
        }
    }
}
