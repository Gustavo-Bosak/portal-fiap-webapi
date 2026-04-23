using PortalFIAP.Application.DTO;

namespace PortalFIAP.Application.Interfaces;

public interface IAlunoService
{
    Task<IEnumerable<AlunoResponse>> GetAllAsync();
    Task<AlunoResponse?> GetByIdAsync(Guid id);
    Task<AlunoResponse> CreateAsync(AlunoRequest request);
    Task<AlunoResponse?> UpdateAsync(Guid id, AlunoRequest request);
    Task<bool> DeleteAsync(Guid id);
}
