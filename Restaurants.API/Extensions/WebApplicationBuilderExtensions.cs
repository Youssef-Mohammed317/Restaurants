using Microsoft.OpenApi;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;
using Serilog;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
            });
            c.AddSecurityRequirement((document) => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("BearerAuth", document)] = []
            });
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();


        builder.Services
            .AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

    }
}
