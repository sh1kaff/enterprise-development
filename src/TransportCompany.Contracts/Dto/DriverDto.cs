using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

public class DriverDto
{
    public required int Id { get; init; }
    public required string PassportNumber { get; init; }
    public required string FullName { get; init; }
    public required int Experience { get; init; }
    public required DrivingLicense License { get; init; }
}