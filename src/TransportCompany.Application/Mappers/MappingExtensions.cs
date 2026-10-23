using TransportCompany.Contracts.Dto;
using TransportCompany.Domain.Entities;

namespace TransportCompany.Application.Mappers;

public static class MappingExtensions
{
    // Client
    public static ClientDto ToDto(this Client entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Phone = entity.Phone
    };

    public static Client ToEntity(this ClientEditDto dto) => new()
    {
        Id = 0,
        Name = dto.Name,
        Phone = dto.Phone
    };

    // Driver
    public static DriverDto ToDto(this Driver entity) => new()
    {
        Id = entity.Id,
        PassportNumber = entity.PassportNumber,
        FullName = entity.FullName,
        Experience = entity.Experience,
        License = entity.License
    };

    public static Driver ToEntity(this DriverEditDto dto) => new()
    {
        Id = 0,
        PassportNumber = dto.PassportNumber,
        FullName = dto.FullName,
        Experience = dto.Experience,
        License = dto.License
    };

    // VehicleModel
    public static VehicleModelDto ToDto(this VehicleModel entity) => new()
    {
        Id = entity.Id,
        BodyType = entity.BodyType,
        BodyVolume = entity.BodyVolume
    };

    public static VehicleModel ToEntity(this VehicleModelEditDto dto) => new()
    {
        Id = 0,
        BodyType = dto.BodyType,
        BodyVolume = dto.BodyVolume
    };

    // Vehicle
    public static VehicleDto ToDto(this Vehicle entity) => new()
    {
        Id = entity.Id,
        LicensePlate = entity.LicensePlate,
        LoadCapacity = entity.LoadCapacity,
        ModelId = entity.ModelId
    };

    public static Vehicle ToEntity(this VehicleEditDto dto) => new()
    {
        Id = 0,
        LicensePlate = dto.LicensePlate,
        LoadCapacity = dto.LoadCapacity,
        ModelId = dto.ModelId
    };

    // Trip
    public static TripDto ToDto(this Trip entity) => new()
    {
        Id = entity.Id,
        DriverId = entity.DriverId,
        VehicleId = entity.VehicleId,
        ClientId = entity.ClientId,
        Date = entity.Date,
        CargoWeight = entity.CargoWeight,
        TransportationCost = entity.TransportationCost,
        Status = entity.Status
    };

    public static Trip ToEntity(this TripEditDto dto) => new()
    {
        Id = 0,
        DriverId = dto.DriverId,
        VehicleId = dto.VehicleId,
        ClientId = dto.ClientId,
        Date = dto.Date,
        CargoWeight = dto.CargoWeight,
        TransportationCost = dto.TransportationCost,
        Status = dto.Status
    };
}