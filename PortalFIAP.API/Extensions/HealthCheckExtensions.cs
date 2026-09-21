using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PortalFiap.Infrastructure.Persistence;

namespace PortalFiap.Extensions;

public static class HealthCheckExtensions
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true
    };

    /// <summary>Registra os health checks: "self" (processo no ar) e "database" (conectividade com o banco).</summary>
    public static IServiceCollection AddPortalHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy("API no ar."))
            // AddDbContextCheck usa CanConnectAsync por padrão: testa conectividade de verdade.
            .AddDbContextCheck<PortalFiapContext>("database");
        return services;
    }

    /// <summary>Mapeia somente GET /health com resposta JSON própria.</summary>
    public static IEndpointRouteBuilder MapPortalHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            },
            ResponseWriter = EscreverRespostaAsync
        }).WithMetadata(new HttpMethodMetadata(new[] { HttpMethods.Get }));

        return endpoints;
    }

    private static Task EscreverRespostaAsync(HttpContext context, HealthReport report)
    {
        var env = context.RequestServices.GetRequiredService<IHostEnvironment>();

        var resposta = new
        {
            status = report.Status.ToString(),
            totalDuration = report.TotalDuration.TotalMilliseconds,
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                duration = e.Value.Duration.TotalMilliseconds,
                description = e.Value.Description,
                // Detalhe da exceção somente em Development.
                exception = env.IsDevelopment() ? e.Value.Exception?.Message : null
            })
        };

        context.Response.ContentType = "application/json; charset=utf-8";
        return context.Response.WriteAsync(JsonSerializer.Serialize(resposta, JsonOptions));
    }
}
