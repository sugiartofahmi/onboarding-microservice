using DotNetService.Constants.Logger;
using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Product.Requests;
using DotNetService.Domain.Product.Services;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Product.Listeners
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

        public async Task<IDictionary<string, object>> Reply(IDictionary<string, object> data)
        {
            var request = Utils.JsonDeserialize<ProductDetailRequest>(Utils.JsonSerialize(data));
            var response = _productService.Detail(request.Id);
            var reply = new Dictionary<string, object> { { "Data", response }, };

            return reply;
        }
    }
}
