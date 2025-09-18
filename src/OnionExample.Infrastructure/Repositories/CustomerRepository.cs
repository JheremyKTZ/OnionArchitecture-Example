using System.Collections.Concurrent;
using OnionExample.Domain.Entities;
using OnionExample.Domain.Interfaces;

namespace OnionExample.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ConcurrentDictionary<Guid, Customer> _store = new();

    public Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IEnumerable<Customer>>(_store.Values);

    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_store.TryGetValue(id, out var customer) ? customer : null);

    public Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        _store[customer.Id] = customer;
        return Task.FromResult(customer);
    }

    public Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        if (!_store.ContainsKey(customer.Id)) return Task.FromResult(false);
        _store[customer.Id] = customer;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(_store.TryRemove(id, out _));
}
