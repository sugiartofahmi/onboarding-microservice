using DotNetService.Constants.Logger;
using DotNetService.Domain.Inventory.Requests;
using DotNetService.Domain.Inventory.Services;
using DotNetService.Domain.Logging.Services;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Inventory.Listeners
{
    public class ProductDetailListener(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        ProductService productService
    ) : IReplyAction<IDictionary<string, object>, IDictionary<string, object>>
    {
        public readonly ILogger _logger = loggerFactory.CreateLogger(LoggerConstant.NATS);
        public readonly LoggingService _loggingService = loggingService;
        private readonly ProductService _productService = productService;

        public IDictionary<string, object> Reply(IDictionary<string, object> data)
        {
            var request = Utils.JsonDeserialize<ProductDetailRequest>(Utils.JsonSerialize(data));
            var response = _productService.Detail(request.Id);
            var reply = new Dictionary<string, object> { { "Data", response }, };

            return reply;
        }
    }
}
