using Kolmeo.Application.Interfaces;
using Kolmeo.Infrastructure.Persistence.Context;
using Kolmeo.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductsDbContext>(options => options.UseInMemoryDatabase("ProductDb"));
            services.AddTransient<IProductRepository, ProductRepository>();
            return services;
        }
    }
}