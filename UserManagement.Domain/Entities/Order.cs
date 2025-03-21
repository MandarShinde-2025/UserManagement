namespace UserManagement.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    public virtual List<OrderItem>? OrderItems { get; set; }
    public decimal TotalAmount { get; set; }
}