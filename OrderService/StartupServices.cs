using DotNetService.Domain.Logging.Services;
using DotNetService.Domain.Order.Services;

namespace DotNetService
{
    public partial class Startup
    {
        public void Services(IServiceCollection services)
        {
            services.AddScoped<LoggingService>();
            services.AddScoped<OrderService>();
        }
    }
}
