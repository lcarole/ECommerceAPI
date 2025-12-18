using API_E_Commerce.Contexts;
using API_E_Commerce.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApi();

// Add db services
builder.Services.AddDbContext<ECommerceContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase"));
});

// Add services by extension methods
builder.Services.AddApplicationServices();

//Add authentication and authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Keycloak:Authority"];
        options.Audience = builder.Configuration["Keycloak:Audience"];
        if (builder.Environment.IsDevelopment())
        {
            options.RequireHttpsMetadata = false;
        }
    });

builder.Services.AddAuthorizationBuilder();

builder.Services.AddMemoryCache();

builder.Services.AddRoutePrefixConvention("api");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();

app.MapScalarApiReference(options => options
    .WithTitle("E-Commerce API")
    .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Fetch)
);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
