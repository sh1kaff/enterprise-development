using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

public class DriverEditDto
{
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [MaxLength(20, ErrorMessage = "Номер паспорта не может превышать 20 символов")]
    public required string PassportNumber { get; init; }

    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(100, ErrorMessage = "ФИО не может превышать 100 символов")]
    public required string FullName { get; init; }

    [Range(0, 70, ErrorMessage = "Стаж должен быть от 0 до 70 лет")]
    public required int Experience { get; init; }

    public required DrivingLicense License { get; init; }
}