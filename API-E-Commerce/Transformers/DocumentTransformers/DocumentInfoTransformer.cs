using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace API_E_Commerce.Transformers.DocumentTransformers;

public class DocumentInfoTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info = new()
        {
            Title = "E-Commerce API",
            Version = "1.0.0",
            Description = """
            API pour un exemple d'application e-commerce.
            Authentification via OAuth2
            """,
        };
        return Task.CompletedTask;
    }
}