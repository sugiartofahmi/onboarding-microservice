using DotNetService.Domain.Logging.Services;

namespace DotNetService
{
    public partial class Startup
    {
        public void Services(IServiceCollection services)
        {
            services.AddScoped<LoggingService>();
        }
    }
}
