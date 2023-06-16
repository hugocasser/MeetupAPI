using System.Reflection;
using Meetup.Info.Application.Commons.Mappings;
using Meetup.Info.Application.Interfaces;
using Meetup.Info.Interfaces;
using Meetup.Info.Persistence;
using Meetup.Info.Services;

namespace Meetup.Info.Extensions;

public static class ServiceMiddleWareExtensions
{
    public static WebApplicationBuilder RegisterServiceMiddleware(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });
        
        builder.Services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(IEventRepository).Assembly));
        });

        builder.Services
            .AddApplication()
            .AddPersistence(builder.Configuration);
            //.AddServices();
        
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
            var authorDbContext = serviceProvider.GetRequiredService<EventInformationDbContext>();
            authorDbContext.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            //
        }

        return app;
    }
}