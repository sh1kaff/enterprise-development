using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

public class VehicleModelEditDto
{
    public required BodyType BodyType { get; init; }

    [Range(0, 200, ErrorMessage = "Объем кузова должен быть положительным (но до 200 м^2)")]
    public required double BodyVolume { get; init; }
}