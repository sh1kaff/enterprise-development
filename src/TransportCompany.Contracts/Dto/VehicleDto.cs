namespace TransportCompany.Contracts.Dto;

public class VehicleDto
{
    public required int Id { get; init; }
    public required string LicensePlate { get; init; }
    public double? LoadCapacity { get; init; }
    public int? ModelId { get; init; }
}