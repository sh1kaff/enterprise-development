using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

public interface IAnalyticsService
{
    public Task<IEnumerable<ClientDto>> GetClientsByVehicleModelAsync(int modelId, CancellationToken cancellationToken = default);

    public Task<IEnumerable<VehicleDto>> GetVehiclesInTransitAsync(CancellationToken cancellationToken = default);
    
    public Task<IEnumerable<TopDriverDto>> GetTop5DriversAsync(CancellationToken cancellationToken = default);
    
    public Task<IEnumerable<VehicleTripCountDto>> GetTripCountForVehiclesAsync(CancellationToken cancellationToken = default);
    
    public Task<IEnumerable<TopClientDto>> GetTop5ClientsByCostAsync(CancellationToken cancellationToken = default);
}