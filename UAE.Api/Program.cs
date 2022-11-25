using Microsoft.Extensions.Options;
using UAE.Api.Extensions;
using UAE.Api.Settings;
using UAE.Application.Extensions;
using UAE.Infrastructure.Data.Init;

var builder = WebApplication.CreateBuilder(args);
var configSection = builder.Configuration.GetSection(nameof(Settings));
builder.Services.Configure<Settings>(configSection);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation();
builder.Services.AddCustomExceptionHandler();
builder.Services.AddLoggerService();
builder.Services.AddServices();

var app = builder.Build();
var settings = app.Services.GetRequiredService<IOptions<Settings>>().Value;

await InitDatabase.InitAsync(settings.Database.Name, settings.Database.Host);

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();