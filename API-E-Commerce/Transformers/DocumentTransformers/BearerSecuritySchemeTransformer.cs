using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace API_E_Commerce.Transformers.DocumentTransformers;
internal sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider, IConfiguration configuration) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authenticationSchemes.Any(authScheme => authScheme.Name == JwtBearerDefaults.AuthenticationScheme))
        {

            var securitySchemes = new Dictionary<string, IOpenApiSecurityScheme>
            {
                ["OAuth2"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(configuration["OAuth2Endpoints:AuthorizationUrl"] 
                            ?? throw new InvalidOperationException("OAuth2Endpoints:AuthorizationUrl is not configured")),

                            TokenUrl = new Uri(configuration["OAuth2Endpoints:TokenUrl"] 
                            ?? throw new InvalidOperationException("OAuth2Endpoints:TokenUrl is not configured"))
                        },
                    }
                }
            };

            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = securitySchemes;
            document.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("OAuth2"),
                        []
                    }
                }
            };

            document.SetReferenceHostDocument();
        }
    }
}