using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.InMemory.Data;

namespace TransportCompany.Infrastructure.InMemory.Repositories;

public class TripRepository : ITripRepository
{
    private readonly InMemoryDbContext _context;

    public TripRepository(InMemoryDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Trip>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Trips.AsEnumerable());
    }

    public Task<Trip?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var trip = _context.Trips.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(trip);
    }

    public Task<Trip> AddAsync(Trip entity, CancellationToken cancellationToken = default)
    {
        entity.Id = _context.Trips.Count == 0 ? 0 : _context.Trips.Max(c => c.Id) + 1;
        
        _context.Trips.Add(entity);
        return Task.FromResult(entity);
    }

    // todo
    public Task UpdateAsync(Trip entity, CancellationToken cancellationToken = default)
    {
        var existing = _context.Trips.FirstOrDefault(c => c.Id == entity.Id);
        if (existing != null)
        {
            existing.Name = entity.Name;
            existing.Phone = entity.Phone;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = _context.Trips.FirstOrDefault(c => c.Id == id);
        if (existing != null)
        {
            _context.Trips.Remove(existing);
        }
        return Task.CompletedTask;
    }
}