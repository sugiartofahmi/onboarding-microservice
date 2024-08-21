using DotNetService.Constants.Event;
using DotNetService.Constants.Logger;
using DotNetService.Domain.Inventory.Requests;
using DotNetService.Domain.Inventory.Services;
using DotNetService.Domain.Logging.Services;
using DotNetService.Http.API.Version1.Inventory.Requests;
using DotNetService.Infrastructure.Integrations.NATs;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Inventory.Listeners
{
    public class ProductAvailabilityListener(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        ProductService productService,
        NATsIntegration _natsIntegration
    ) : ISubscriptionAction<IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;

        private readonly ProductService _productService = productService;

        private readonly NATsIntegration _natsIntegration = _natsIntegration;

        public void Handle(IDictionary<string, object> data)
        {
            var jsonData = Utils.JsonSerialize(data);
            var request = Utils.JsonDeserialize<ProductCheckAvailabilityRequest>(jsonData);
            Guid ProductId = new Guid(request.ProductId);
            var product = _productService.Detail(ProductId);
            var response = new ProductCheckAvailabilityResponse { OrderId = request.OrderId, };

            if (product is null || product.Stock < request.Quantity)
            {
                response.IsProductAvailable = false;
            }
            else
            {
                var updateProduct = new ProductUpdateRequest
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = (int)product.Price,
                    Stock = (int)(product.Stock - request.Quantity)
                };
                _productService.Update(product.Id, updateProduct);
                response.IsProductAvailable = true;
            }

            string subject = _natsIntegration.Subject(
                NATsEventModuleEnum.ORDER,
                NATsEventActionEnum.UPDATE_STATUS,
                NATsEventStatusEnum.SUCCESS
            );

            _ = _natsIntegration.Publish<string>(subject, Utils.JsonSerialize(response));

            _logger.LogInformation(jsonData);
        }
    }
}
