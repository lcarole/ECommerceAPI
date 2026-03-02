using API_E_Commerce.Endpoints;

namespace API_E_Commerce.Extensions;

public static class ApiEndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        // Group endpoints with common prefix
        var apiGroup = app.MapGroup("api/v1")
            .RequireAuthorization();

        // Map Items endpoints
        apiGroup.MapGroup("items")
            .MapItemsEndpoints()
            .WithTags("Items");

        // Map Categories endpoints
        apiGroup.MapGroup("categories")
            .MapCategoriesEndpoints()
            .WithTags("Categories");

        return app;
    }
}
