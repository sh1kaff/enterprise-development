namespace TransportCompany.Contracts.Dto;

public class VehicleDto
{
    public required int Id { get; init; }
    public required string LicensePlate { get; init; }
    public required double LoadCapacity { get; init; }
    public required int ModelId { get; init; }
}