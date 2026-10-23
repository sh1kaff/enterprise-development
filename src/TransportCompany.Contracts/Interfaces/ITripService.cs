using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

public interface ITripService
{
    public Task<IEnumerable<TripDto>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<TripDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    public Task<TripDto> CreateAsync(TripEditDto dto, CancellationToken cancellationToken = default);
    public Task UpdateAsync(int id, TripEditDto dto, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}