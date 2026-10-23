using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

public class VehicleModelDto
{
    public required int Id { get; init; }
    public required BodyType BodyType { get; init; }
    public required double BodyVolume { get; init; }
}