using API_E_Commerce.Contexts;
using API_E_Commerce.Transformers.DocumentTransformers;
using API_E_Commerce.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApi(options => 
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    options.AddDocumentTransformer<DocumentInfoTransformer>();
});

// Add db services
builder.Services.AddDbContext<ECommerceContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

// Add services by extension methods
builder.Services.AddApplicationServices();

//Add authentication and authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["JwtAuthentication:Authority"];
        options.Audience = builder.Configuration["JwtAuthentication:Audience"];
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("Issuer: " + context.Options.Authority);
                Console.WriteLine("Audience: " + context.Options.Audience);
                Console.WriteLine($"❌ Auth failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("✅ Token validated");
                return Task.CompletedTask;
            }
        };
        
        if (builder.Environment.IsDevelopment())
        {
            options.RequireHttpsMetadata = false;
        }
    });

builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options => options
        .WithTitle("E-Commerce API")
        .AddPreferredSecuritySchemes("Bearer")
    );
}

app.UseForwardedHeaders();

app.UseAuthentication();

app.UseAuthorization();

app.MapApiEndpoints();

app.Run();