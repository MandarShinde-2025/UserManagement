namespace UserManagement.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int UserId { get; set; }
    public virtual List<OrderItemDto> OrderItems { get; set; } = [];
    public decimal TotalAmount { get; set; }
}