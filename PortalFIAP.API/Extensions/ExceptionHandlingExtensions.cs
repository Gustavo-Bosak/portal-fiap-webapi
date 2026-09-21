using PortalFiap.Exceptions;

namespace PortalFiap.Extensions;

public static class ExceptionHandlingExtensions
{
    /// <summary>Registra o GlobalExceptionHandler e o suporte a ProblemDetails.</summary>
    public static IServiceCollection AddPortalExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }

    /// <summary>Ativa o middleware de tratamento de exceções (deve vir antes de MapControllers).</summary>
    public static WebApplication UsePortalExceptionHandling(this WebApplication app)
    {
        app.UseExceptionHandler();
        return app;
    }
}
