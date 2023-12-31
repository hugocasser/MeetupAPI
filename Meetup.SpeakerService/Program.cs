using Meetup.SpeakerService.Extensions;
using Meetup.SpeakerService.Middleware;
using Meetup.SpeakerService.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServiceMiddleware();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");
app.InitializeServiceContextProvider();
using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
try
{
    var eventInfoDbContext = serviceProvider.GetRequiredService<SpeakerDbContext>();
    eventInfoDbContext.Database.EnsureCreated();
    
}
catch (Exception ex)
{
    
}
app.Run();