using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

public interface IVehicleService
{
    public Task<IEnumerable<VehicleDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<VehicleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<VehicleDto> CreateAsync(VehicleEditDto dto, CancellationToken cancellationToken = default);
    public Task UpdateAsync(int id, VehicleEditDto dto, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}