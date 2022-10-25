using BookingTables.API.ServicesConfiguration;
using BookingTablesAPI.ServiceExtensions;
using BookingTablesAPI.ServicesConfiguration;
using Core.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

//IdentityModelEventSource.ShowPII = true;
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureAutomapper();
builder.Services.ConfigureAppServices();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAppSettings(builder.Configuration);
builder.Services.ConfigureFirebaseDatabase(builder.Configuration);
builder.Services.ConfigureStorage();
builder.Services.ConfigureCQRS();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureFluentValidation();
builder.Services.ConfigureCaching();
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.ConfigureVersioning();
builder.Logging.ConfigureSerilog(builder.Configuration);
builder.Services.ConfigureQuartz(builder.Configuration);
builder.Services.ConfigureElasticsearch(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);


var app = builder.Build();

app.MigrateDatabase();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers() ;
app.MapHub<OnlineAssistant>("/assistant");
app.MapHealthChecks("/health",new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});
app.Run();
