namespace TransportCompany.Contracts.Dto;

public class TopDriverDto
{
    // мб еще что то добавить потом но пока так
    public required int DriverId { get; init; }
    public required string DriverName { get; init; }
    public required int TripCount { get; init; }
}