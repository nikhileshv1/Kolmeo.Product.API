using Kolmeo.Application.Interfaces;
using Kolmeo.Application.MapProfiles;
using Kolmeo.Application.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {            
            services.AddScoped<IProductsService, ProductsService>();
            services.AddAutoMapper(typeof(ProductProfile));
            return services;
        }
    }
}