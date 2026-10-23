using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

public interface IVehicleModelService
{
    public Task<IEnumerable<VehicleModelDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<VehicleModelDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<VehicleModelDto> CreateAsync(VehicleModelEditDto dto, CancellationToken cancellationToken = default);
    public Task UpdateAsync(int id, VehicleModelEditDto dto, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}