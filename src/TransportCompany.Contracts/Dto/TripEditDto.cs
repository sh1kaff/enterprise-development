using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

public class TripEditDto
{
    [Required] public required int DriverId { get; init; }
    [Required] public required int VehicleId { get; init; }
    [Required] public required int ClientId { get; init; }

    [Required]
    public required DateTime Date { get; init; }

    [Range(0.1, 100, ErrorMessage = "Вес груза должен быть положительным (от 0.1 до 100)")]
    public required double CargoWeight { get; init; }

    [Range(0, 10000000, ErrorMessage = "Некорректная стоимость")]
    public decimal? TransportationCost { get; init; }

    public required TripStatus Status { get; init; }
}