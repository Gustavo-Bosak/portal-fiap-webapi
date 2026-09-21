using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortalFiap.Domain.Exceptions;

namespace PortalFiap.Exceptions;

/// <summary>
/// Handler global de exceções: converte qualquer exceção não tratada em ProblemDetails (RFC 7807).
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private const string MensagemGenerica = "Ocorreu um erro interno.";

    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (status, title) = Mapear(exception);

        if (status >= StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(exception,
                "Erro não tratado em {Method} {Path}. TraceId {TraceId}",
                httpContext.Request.Method, httpContext.Request.Path, httpContext.TraceIdentifier);
        }
        else
        {
            _logger.LogWarning(
                "Requisição rejeitada em {Method} {Path}: {Status} {ExceptionType} - {Mensagem}. TraceId {TraceId}",
                httpContext.Request.Method, httpContext.Request.Path, status,
                exception.GetType().Name, exception.Message, httpContext.TraceIdentifier);
        }

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = ObterDetalhe(status, exception),
            Instance = httpContext.Request.Path
        };
        problem.Extensions["traceId"] = httpContext.TraceIdentifier;

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(
            problem, options: null, contentType: "application/problem+json", cancellationToken);

        return true;
    }

    private static (int Status, string Title) Mapear(Exception exception) => exception switch
    {
        ResourceNotFoundException or KeyNotFoundException
            => (StatusCodes.Status404NotFound, "Recurso não encontrado."),
        ConflictException
            => (StatusCodes.Status409Conflict, "Conflito de dados."),
        DomainException or ArgumentException
            => (StatusCodes.Status400BadRequest, "Requisição inválida."),
        BadHttpRequestException or JsonException
            => (StatusCodes.Status400BadRequest, "Payload inválido."),
        _ => (StatusCodes.Status500InternalServerError, "Erro interno do servidor.")
    };

    private string ObterDetalhe(int status, Exception exception)
    {
        if (status < StatusCodes.Status500InternalServerError)
        {
            // JsonException/BadHttpRequest podem expor detalhes internos do parser: mensagem genérica.
            return exception is JsonException or BadHttpRequestException
                ? "O corpo da requisição é inválido."
                : exception.Message;
        }

        // Nunca expor detalhes (stack trace, banco) fora de Development.
        return _environment.IsDevelopment() ? exception.Message : MensagemGenerica;
    }
}
