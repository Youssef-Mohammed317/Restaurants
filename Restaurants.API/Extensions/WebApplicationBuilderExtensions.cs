using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Seeds.Seeders.Helpers;
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
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();


        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        builder.Services.AddOptions<SeedUsersOptions>()
            .Bind(builder.Configuration.GetSection("SeedUsers"))
            .Validate(options =>
            {
                bool IsValidUser(SeedUser user) =>
                    !string.IsNullOrWhiteSpace(user.Email) &&
                    !string.IsNullOrWhiteSpace(user.UserName) &&
                    !string.IsNullOrWhiteSpace(user.Password);
                return IsValidUser(options.Admin) &&
                       IsValidUser(options.Owner) &&
                       IsValidUser(options.User);
            }, "Each seed user must have Email, UserName, and Password defined.");
    }
}
