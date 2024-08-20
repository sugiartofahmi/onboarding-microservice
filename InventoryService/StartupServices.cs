using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Product.Services;

namespace DotNetService
{
    public partial class Startup
    {
        public void Services(IServiceCollection services)
        {
            services.AddScoped<ProductService>();
            services.AddScoped<LoggingService>();
        }
    }
}
