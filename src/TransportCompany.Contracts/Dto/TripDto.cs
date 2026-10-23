using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

public class TripDto
{
    public required int Id { get; init; }
    public required int DriverId { get; init; }
    public required int VehicleId { get; init; }
    public required int ClientId { get; init; }
    public required DateTime Date { get; init; }
    public required double CargoWeight { get; init; }
    public required decimal TransportationCost { get; init; }
    public required TripStatus Status { get; init; }
}