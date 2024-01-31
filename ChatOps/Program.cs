using ChatOps.API.Configurations;
using ChatOps.Services.Hubs;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ServiceExtensions.ServiceConfig(builder.Services);
AutoMapperConfig.Configure(builder.Services);
SwaggerConfig.ConfigureServices(builder.Services, config);
AuthNConfig.ConfigureServices(builder.Services, config);
DatabaseConfig.ConfigureServices(builder.Services, builder.Configuration);
builder.Services.AddHostedService<DataSeedr>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();
SwaggerConfig.Configure(app, config);
DatabaseConfig.Configure(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<HubService>("/signalhub");
app.Run();