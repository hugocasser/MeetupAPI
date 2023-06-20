using System.Reflection;
using Meetup.Application;
using Meetup.Application.Common.Mappings;
using Meetup.Application.Interfaces;
using Meetup.Configuration;
using Meetup.Persistence.Repositories;
using Meetup.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Meetup.Extensions;

public static class ServiceMiddlewareExtensions
{
    public static WebApplicationBuilder RegisterServiceMiddleware(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = "Modsen Library",
                Description = "An ASP.NET Web api for library service"
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.EnableAnnotations();
        });
        
        builder.Services
            .AddServices()
            .AddApplication()
            .AddPersistence(builder.Configuration);
        
        builder.Services
            .AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = AuthConfiguration.GetSymmetricSecurityKeyStatic()
                };
            });

        builder.Services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(IUserRepository).Assembly));
        });
        
        builder.Services.AddCors(options =>
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            }));
        
        return builder;
    }
    
    public static WebApplication InitializeServiceContextProvider(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var authorDbContext = serviceProvider.GetRequiredService<MeetUpDbContext>();
            authorDbContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            //
        }

        return app;
    }
}