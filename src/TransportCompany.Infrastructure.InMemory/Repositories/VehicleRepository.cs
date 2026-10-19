using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.InMemory.Data;

namespace TransportCompany.Infrastructure.InMemory.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly InMemoryDbContext _context;

    public VehicleRepository(InMemoryDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Vehicles.AsEnumerable());
    }

    public Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var vehicle = _context.Vehicles.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(vehicle);
    }

    public Task<Vehicle> AddAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        entity.Id = _context.Vehicles.Count == 0 ? 0 : _context.Vehicles.Max(c => c.Id) + 1;
        
        _context.Vehicles.Add(entity);
        return Task.FromResult(entity);
    }

    // todo
    public Task UpdateAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        var existing = _context.Vehicles.FirstOrDefault(c => c.Id == entity.Id);
        if (existing != null)
        {
            existing.Name = entity.Name;
            existing.Phone = entity.Phone;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = _context.Vehicles.FirstOrDefault(c => c.Id == id);
        if (existing != null)
        {
            _context.Vehicles.Remove(existing);
        }
        return Task.CompletedTask;
    }
}