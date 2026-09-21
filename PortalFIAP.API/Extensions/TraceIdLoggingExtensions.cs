namespace PortalFiap.Extensions;

public static class TraceIdLoggingExtensions
{
    private const string HeaderName = "X-Trace-Id";

    /// <summary>
    /// Abre um logger scope com o TraceId de cada requisição e devolve o header X-Trace-Id.
    /// Deve ser um dos primeiros middlewares do pipeline.
    /// </summary>
    public static IApplicationBuilder UseTraceIdLogging(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            var traceId = context.TraceIdentifier;
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Trace-Id"] = traceId;
                return Task.CompletedTask;
            });

            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                .CreateLogger("PortalFiap.TraceId");

            using (logger.BeginScope("TraceId:{TraceId}", traceId))
            {
                await next();
            }
        });
    }
}
