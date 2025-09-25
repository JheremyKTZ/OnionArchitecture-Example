using OnionExample.Domain.Entities;
using OnionExample.Domain.Interfaces;

namespace OnionExample.Application.Services;

public class CustomerService(ICustomerRepository repository) : ICustomerService
{
    private readonly ICustomerRepository _repository = repository;

    public Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        => _repository.GetAllAsync(cancellationToken);

    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.GetByIdAsync(id, cancellationToken);

    public Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
        => _repository.AddAsync(customer, cancellationToken);

    public Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
        => _repository.UpdateAsync(customer, cancellationToken);

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);
}
