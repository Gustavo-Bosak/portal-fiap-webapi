namespace PortalFiap.Domain.Exceptions;

/// <summary>
/// Recurso solicitado não existe ou está inativo (mapeada para HTTP 404).
/// </summary>
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string message) : base(message) { }

    public ResourceNotFoundException(string resource, Guid id)
        : base($"{resource} com id '{id}' não foi encontrado(a).") { }
}
