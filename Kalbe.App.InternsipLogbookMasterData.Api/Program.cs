using Elastic.Apm.NetCoreAll;
using HealthChecks.UI.Client;
using Kalbe.App.InternsipLogbookMasterData.Api.Auth;
using Kalbe.App.InternsipLogbookMasterData.Api.Models;
using Kalbe.App.InternsipLogbookMasterData.Api.Services;
using Kalbe.Library.Common.Logs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRequiredServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseAllElasticApm(builder.Configuration);


if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("*"));

app.UseMiddleware<ExceptionLogMiddleware>();
app.UseMiddleware<InternsipLogbookMasterDataJwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/hc");
app.MapHealthChecks("/hc-ui", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Force database migration
app.MigrateDbContext<InternsipLogbookMasterDataDataContext>();

app.Run();
