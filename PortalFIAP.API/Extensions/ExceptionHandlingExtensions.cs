using PortalFiap.Exceptions;

namespace PortalFiap.Extensions;

public static class ExceptionHandlingExtensions
{
    /// <summary>Registra o GlobalExceptionHandler e o suporte a ProblemDetails.</summary>
    public static IServiceCollection AddPortalExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        // ProblemDetails gerados pelo framework (404 sem corpo, 400 de validação) usam o mesmo traceId
        // do GlobalExceptionHandler e dos logs (HttpContext.TraceIdentifier), em vez do Activity.Id.
        services.AddProblemDetails(options =>
            options.CustomizeProblemDetails = context =>
                context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier);
        return services;
    }

    /// <summary>Ativa o middleware de tratamento de exceções (deve vir antes de MapControllers).</summary>
    public static WebApplication UsePortalExceptionHandling(this WebApplication app)
    {
        app.UseExceptionHandler();
        return app;
    }
}
