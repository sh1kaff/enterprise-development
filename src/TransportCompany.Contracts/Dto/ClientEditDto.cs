using System.ComponentModel.DataAnnotations;

namespace TransportCompany.Contracts.Dto;

public class ClientEditDto
{
    [Required(ErrorMessage = "Имя требуется обязательно")]
    [MaxLength(100, ErrorMessage = "Длина имени не может быть больше 100")]
    public required string Name { get; init; }

    [Required(ErrorMessage = "Телефон требуется обязательно")]
    [Phone(ErrorMessage = "Неверный формат телефона")]
    [MaxLength(20, ErrorMessage = "Длина номера телефон не может быть больше 20")]
    public required string Phone { get; init; }
}