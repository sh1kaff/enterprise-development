using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

public class TripDto
{
    public required int Id { get; init; }
    public required int DriverId { get; init; }
    public required int VehicleId { get; init; }
    public required int ClientId { get; init; }
    public DateTime? Date { get; init; }
    public double? CargoWeight { get; init; }
    public decimal? TransportationCost { get; init; }
    public TripStatus? Status { get; init; }
}