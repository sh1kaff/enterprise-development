using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Interfaces;
using TransportCompany.Infrastructure.InMemory.Data;

namespace TransportCompany.Infrastructure.InMemory.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly InMemoryDbContext _context;

    public ClientRepository(InMemoryDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_context.Clients.AsEnumerable());
    }

    public Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(client);
    }

    public Task<Client> AddAsync(Client entity, CancellationToken cancellationToken = default)
    {
        entity.Id = _context.Clients.Count == 0 ? 0 : _context.Clients.Max(c => c.Id) + 1;
        
        _context.Clients.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(Client entity, CancellationToken cancellationToken = default)
    {
        var existing = _context.Clients.FirstOrDefault(c => c.Id == entity.Id);
        if (existing != null)
        {
            existing.Name = entity.Name;
            existing.Phone = entity.Phone;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = _context.Clients.FirstOrDefault(c => c.Id == id);
        if (existing != null)
        {
            _context.Clients.Remove(existing);
        }
        return Task.CompletedTask;
    }
}