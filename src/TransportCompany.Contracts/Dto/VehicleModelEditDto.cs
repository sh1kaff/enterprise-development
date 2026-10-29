using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO для создания или редактирования модели транспортного средства
/// </summary>
public class VehicleModelEditDto
{
    /// <summary>
    /// Тип кузова
    /// </summary>
    public required BodyType BodyType { get; init; }

    /// <summary>
    /// Объём кузова в м^3
    /// </summary>
    [Range(0, 200, ErrorMessage = "Объем кузова должен быть положительным (но до 200 м^2)")]
    public required double BodyVolume { get; init; }
}