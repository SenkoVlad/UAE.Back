using Microsoft.Extensions.Options;
using UAE.Api.Extensions;
using UAE.Application.Extensions;
using UAE.Infrastructure.Data.Init;
using UAE.Infrastructure.Extensions;
using UAE.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions<Settings>()
    .Bind(builder.Configuration.GetSection(nameof(Settings)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidation();
builder.Services.AddCustomExceptionHandler();
builder.Services.AddLoggerService();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddJwtAuth(builder.Configuration);

var app = builder.Build();
var settings = app.Services.GetRequiredService<IOptions<Settings>>().Value;

await InitDatabase.InitAsync(settings.Database.Name, settings.Database.Host);

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomException();
app.UseSession();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();