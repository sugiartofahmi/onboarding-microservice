using DotNetService.Constants.Logger;
using DotNetService.Domain.Inventory.Requests;
using DotNetService.Domain.Inventory.Services;
using DotNetService.Domain.Logging.Services;
using DotNetService.Infrastructure.Shareds;
using DotNetService.Infrastructure.Subscriptions;

namespace DotNetService.Domain.Inventory.Listeners
{
    public class ProductListListener(
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
            var request = Utils.JsonDeserialize<ProductQueryRequest>(Utils.JsonSerialize(data));
            var response = _productService.Index(request);

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
