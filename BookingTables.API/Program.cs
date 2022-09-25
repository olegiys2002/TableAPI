using BookingTablesAPI.ServiceExtensions;
using BookingTablesAPI.ServicesConfiguration;
using Core.Services;


var builder = WebApplication.CreateBuilder(args);

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



var app = builder.Build();

app.MigrateDatabase();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers() ;
app.MapHub<OnlineAssistant>("/assistant");

app.Run();
