using DotNetService.Domain.Logging.Listeners;
using DotNetService.Domain.Order.Listeners;

namespace DotNetService
{
    public partial class Startup
    {
        public void Listeners(IServiceCollection services)
        {
            services.AddScoped<LoggingNATsListen>();
            services.AddScoped<LoggingNATsListenAndReply>();
            services.AddScoped<OrderUpdateStatusListener>();
        }
    }
}
