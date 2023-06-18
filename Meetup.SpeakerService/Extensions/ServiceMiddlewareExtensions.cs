using System.Reflection;
using Meetup.SpeakerService.Application;
using Meetup.SpeakerService.Application.Common.Mapping;
using Meetup.SpeakerService.Application.Interfaces;
using Meetup.SpeakerService.Middleware;
using Meetup.SpeakerService.Persistence;
using Meetup.SpeakerService.Services;

namespace Meetup.SpeakerService.Extensions;

public static class ServiceMiddlewareExtensions
{
    public static WebApplicationBuilder RegisterServiceMiddleware(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });
        
        builder.Services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(ISpeakerRepository).Assembly));
        });
        
        builder.Services
            .AddServices()
            .AddApplication()
            .AddPersistence(builder.Configuration);
        
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
            var speakerDbContext = serviceProvider.GetRequiredService<SpeakerDbContext>();
            speakerDbContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            //
        }

        return app;
    }
}