using DotNetService.Domain.Logging.Listeners;

namespace DotNetService
{
    public partial class Startup
    {
        public void Listeners(IServiceCollection services)
        {
            services.AddScoped<LoggingNATsListen>();
            services.AddScoped<LoggingNATsListenAndReply>();
            // services.AddScoped<OrderCreateListen>();
            // services.AddScoped<OrderListListen>();
            // services.AddScoped<OrderDetailListen>();
        }
    }
}
