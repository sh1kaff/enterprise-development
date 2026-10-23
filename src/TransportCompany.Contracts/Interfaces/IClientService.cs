using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

public interface IClientService
{
    public Task<IEnumerable<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<ClientDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<ClientDto> CreateAsync(ClientEditDto dto, CancellationToken cancellationToken = default);
    public Task UpdateAsync(int id, ClientEditDto dto, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}