using API_E_Commerce.Services;

namespace API_E_Commerce.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryService>();
        services.AddScoped<ItemService>();
        return services;
    }
}