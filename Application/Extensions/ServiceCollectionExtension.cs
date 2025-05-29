using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Services.Abstract;
using Application.Services.Implementation;
using Infrastructure.Extensions;


namespace Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureLayerServices(configuration);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
