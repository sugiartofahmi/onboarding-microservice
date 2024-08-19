using DotNetService.Domain.Order.Repositories;

namespace DotNetService
{
    public partial class Startup
    {
        public void Repositories(IServiceCollection services)
        {
            services.AddScoped<OrderQueryRepository>();
            services.AddScoped<OrderStoreRepository>();
        }
    }
}
