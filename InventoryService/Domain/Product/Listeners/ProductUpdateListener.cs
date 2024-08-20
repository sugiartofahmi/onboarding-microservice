using DotNetService.Constants.Logger;
using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Product.Requests;
using DotNetService.Domain.Product.Services;
using DotNetService.Infrastructure.Shareds;

namespace DotNetService.Domain.Product.Listeners
{
    public class ProductUpdateListener(
        ILoggerFactory loggerFactory,
        LoggingService loggingService,
        ProductService productService
    )
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
