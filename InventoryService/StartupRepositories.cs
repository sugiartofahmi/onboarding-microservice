using DotNetService.Domain.Product.Repositories;

namespace DotNetService
{
    public partial class Startup
    {
        public void Repositories(IServiceCollection services)
        {
            services.AddScoped<ProductQueryRepository>();
            services.AddScoped<ProductStoreRepository>();
        }
    }
}
