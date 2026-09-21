namespace PortalFiap.Domain.Exceptions;

/// <summary>
/// Violação de uma regra de negócio ou invariante do domínio (mapeada para HTTP 400).
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
