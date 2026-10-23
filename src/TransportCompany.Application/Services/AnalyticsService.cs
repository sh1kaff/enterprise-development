using TransportCompany.Application.Mappers;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Enums;
using TransportCompany.Domain.Interfaces;

namespace TransportCompany.Application.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly ITripRepository _tripRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IDriverRepository _driverRepository;

    public AnalyticsService(
        ITripRepository tripRepository,
        IVehicleRepository vehicleRepository,
        IClientRepository clientRepository,
        IDriverRepository driverRepository)
    {
        _tripRepository = tripRepository;
        _vehicleRepository = vehicleRepository;
        _clientRepository = clientRepository;
        _driverRepository = driverRepository;
    }

    public async Task<IEnumerable<ClientDto>> GetClientsByVehicleModelAsync(int modelId, CancellationToken cancellationToken = default)
    {
        var trips = await _tripRepository.GetAllAsync(cancellationToken);
        var vehicles = await _vehicleRepository.GetAllAsync(cancellationToken);
        var clients = await _clientRepository.GetAllAsync(cancellationToken);

        var targetVehicleIds = vehicles.Where(v => v.ModelId == modelId).Select(v => v.Id).ToHashSet();

        var clientIds = trips
            .Where(t => targetVehicleIds.Contains(t.VehicleId))
            .Select(t => t.ClientId)
            .Distinct();

        return clients
            .Where(c => clientIds.Contains(c.Id))
            .OrderBy(c => c.Name)
            .Select(c => c.ToDto());
    }

    public async Task<IEnumerable<VehicleDto>> GetVehiclesInTransitAsync(CancellationToken cancellationToken = default)
    {
        var trips = await _tripRepository.GetAllAsync(cancellationToken);
        var vehicles = await _vehicleRepository.GetAllAsync(cancellationToken);

        var vehiclesInTransitIds = trips
            .Where(t => t.Status == TripStatus.InTransit)
            .Select(t => t.VehicleId)
            .Distinct();

        return vehicles
            .Where(v => vehiclesInTransitIds.Contains(v.Id))
            .Select(v => v.ToDto());
    }

    public async Task<IEnumerable<TopDriverDto>> GetTop5DriversAsync(CancellationToken cancellationToken = default)
    {
        var trips = await _tripRepository.GetAllAsync(cancellationToken);
        var drivers = await _driverRepository.GetAllAsync(cancellationToken);

        var topDriverIds = trips
            .GroupBy(t => t.DriverId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .ToList();

        var result = new List<TopDriverDto>();
        foreach (var group in topDriverIds)
        {
            var driver = drivers.FirstOrDefault(d => d.Id == group.Key);
            if (driver != null)
            {
                result.Add(new TopDriverDto
                {
                    DriverId = driver.Id,
                    DriverName = driver.FullName,
                    TripCount = group.Count()
                });
            }
        }

        return result;
    }

    public async Task<IEnumerable<VehicleTripCountDto>> GetTripCountForVehiclesAsync(CancellationToken cancellationToken = default)
    {
        var trips = await _tripRepository.GetAllAsync(cancellationToken);
        var vehicles = await _vehicleRepository.GetAllAsync(cancellationToken);

        return vehicles.Select(v => new VehicleTripCountDto
        {
            LicensePlate = v.LicensePlate,
            TripCount = trips.Count(t => t.VehicleId == v.Id)
        });
    }

    public async Task<IEnumerable<TopClientDto>> GetTop5ClientsByCostAsync(CancellationToken cancellationToken = default)
    {
        var trips = await _tripRepository.GetAllAsync(cancellationToken);
        var clients = await _clientRepository.GetAllAsync(cancellationToken);

        var topClientStats = trips
            .GroupBy(t => t.ClientId)
            .OrderByDescending(g => g.Sum(t => t.TransportationCost ?? 0m))
            .Take(5)
            .ToList();

        var result = new List<TopClientDto>();
        foreach (var stat in topClientStats)
        {
            var client = clients.FirstOrDefault(c => c.Id == stat.Key);
            if (client != null)
            {
                result.Add(new TopClientDto
                {
                    ClientName = client.Name,
                    TotalCost = stat.Sum(t => t.TransportationCost ?? 0m)
                });
            }
        }

        return result;
    }
}