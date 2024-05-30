namespace Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal Total => Items.Sum(i => i.Quantity * i.Price);
}