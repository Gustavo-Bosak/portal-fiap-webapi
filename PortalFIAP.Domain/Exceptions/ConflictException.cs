namespace PortalFiap.Domain.Exceptions;

/// <summary>
/// Operação conflita com o estado atual do recurso, ex.: duplicidade (mapeada para HTTP 409).
/// </summary>
public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}
