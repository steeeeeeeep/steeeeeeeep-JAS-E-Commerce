using JASApi.Controllers.Products;
using JASData.DomainService.Products;

namespace JASApi.Services;

public static class ServiceRegistration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDomainProductService, DomainProductService>();
    }
}
