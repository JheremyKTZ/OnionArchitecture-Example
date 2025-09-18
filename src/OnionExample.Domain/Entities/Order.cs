namespace OnionExample.Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
}


