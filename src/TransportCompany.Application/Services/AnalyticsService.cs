using TransportCompany.Application.Mappers;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Enums;
using TransportCompany.Domain.Interfaces;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис аналитики транспортной компании
/// </summary>
public class AnalyticsService(
        ITripRepository tripRepository,
        IVehicleRepository vehicleRepository,
        IClientRepository clientRepository,
        IDriverRepository driverRepository) : IAnalyticsService
{
    private readonly ITripRepository _tripRepository = tripRepository;
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IDriverRepository _driverRepository = driverRepository;

    /// <summary>
    /// Получить клиентов, перевозивших грузы на ТС указанной модели
    /// </summary>
    /// <param name="modelId">Идентификатор модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Клиенты, отсортированные по имени</returns>
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

    /// <summary>
    /// Получить транспортные средства, находящиеся в рейсе
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Транспортные средства в пути</returns>
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

    /// <summary>
    /// Получить топ-5 водителей по количеству рейсов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Топ-5 водителей</returns>
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

    /// <summary>
    /// Получить количество рейсов для каждого транспортного средства
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество рейсов по транспортным средствам</returns>
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

    /// <summary>
    /// Получить топ-5 клиентов по суммарной стоимости перевозок
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Топ-5 клиентов</returns>
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