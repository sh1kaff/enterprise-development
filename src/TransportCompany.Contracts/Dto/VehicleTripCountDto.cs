namespace TransportCompany.Contracts.Dto;

public class VehicleTripCountDto
{
    public required string LicensePlate { get; init; }
    public required int TripCount { get; init; }
}