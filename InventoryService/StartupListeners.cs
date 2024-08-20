using DotNetService.Domain.Logging.Listeners;
using DotNetService.Domain.Product.Listeners;

namespace DotNetService
{
    public partial class Startup
    {
        public void Listeners(IServiceCollection services)
        {
            services.AddScoped<LoggingNATsListen>();
            services.AddScoped<LoggingNATsListenAndReply>();
            services.AddScoped<ProductCreateListener>();
            services.AddScoped<ProductUpdateListener>();
            services.AddScoped<ProductListListener>();
            services.AddScoped<ProductDetailListener>();
        }
    }
}
