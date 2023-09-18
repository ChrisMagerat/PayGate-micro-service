using PayGateMicroService.API.Common;
using PayGateMicroService.API.Configuration;
using PayGateMicroService.Application.Common;
using PayGateMicroService.Infrastructure.Common;

using Prometheus;


var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.GetAppSettings();

builder.AddApi();
builder.AddInfrastructure(appSettings);
builder.Services.AddApplication();
builder.Services.Add3rdPartyIntegrations(builder.Configuration);


var app = builder.Build();
app.HttpRequestPipeline(appSettings.AuthenticationConfiguration);
app.UseHttpsRedirection();
app.UseMetricServer();

app.UseAuthorization();
app.UseAuthentication();
app.UseIdentityServer();

app.MapControllers();

await app.UseBlobStorage();
await app.Migrate(args);

app.Run();
