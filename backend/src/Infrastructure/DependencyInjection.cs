using JAS.ECommerce.Application.Mapping;
using JAS.ECommerce.Application.Validators.Product;
using JAS.ECommerce.Domain.Interfaces;
using JAS.ECommerce.Infrastructure.Persistence;
using JAS.ECommerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;

namespace JAS.ECommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<ECommerceDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
