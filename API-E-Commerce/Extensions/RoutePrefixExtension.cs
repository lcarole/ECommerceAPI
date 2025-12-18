using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace API_E_Commerce.Extensions;

public static class RoutePrefixExtension
{
    public static IServiceCollection AddRoutePrefixConvention(this IServiceCollection services, string prefix)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteConvention(new AttributeRouteModel { Template = prefix }));
        });
        
        return services;
    }
}

public class RouteConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    public RouteConvention(AttributeRouteModel routePrefix)
    {
        _routePrefix = routePrefix;
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
            
            if (matchedSelectors.Any())
            {
                foreach (var selectorModel in matchedSelectors)
                {
                    selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(
                        _routePrefix,
                        selectorModel.AttributeRouteModel);
                }
            }

            var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
            
            if (unmatchedSelectors.Any())
            {
                foreach (var selectorModel in unmatchedSelectors)
                {
                    selectorModel.AttributeRouteModel = _routePrefix;
                }
            }
        }
    }
}