using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

public interface IDriverService
{
    public Task<IEnumerable<DriverDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<DriverDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<DriverDto> CreateAsync(DriverEditDto dto, CancellationToken cancellationToken = default);
    public Task UpdateAsync(int id, DriverEditDto dto, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}