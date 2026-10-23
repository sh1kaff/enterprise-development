using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Contracts.Dto;

public class VehicleEditDto
{
    [Required(ErrorMessage = "Гос. номер обязателен")]
    [MaxLength(8, ErrorMessage = "Гос. номер не может превышать 8 символов")]
    public required string LicensePlate { get; init; }

    [Range(0.1, 100, ErrorMessage = "Грузоподъемность должна быть от 0.1 до 100 тонн")]
    public required double LoadCapacity { get; init; }

    [Required(ErrorMessage = "Модель ТС обязательна")]
    public required int ModelId { get; init; }
}