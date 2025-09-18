using Microsoft.AspNetCore.Mvc;
using OnionExample.Application.Services;
using OnionExample.Domain.Entities;

namespace OnionExample.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(CustomerService service) : ControllerBase
{
    private readonly CustomerService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Customer>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var customer = await _service.GetByIdAsync(id, cancellationToken);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> Create(Customer customer, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(customer, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, Customer customer, CancellationToken cancellationToken)
    {
        if (id != customer.Id) return BadRequest("Id mismatch");
        var ok = await _service.UpdateAsync(customer, cancellationToken);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var ok = await _service.DeleteAsync(id, cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}


