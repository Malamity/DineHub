namespace Domain.Entities

{
    public class Order
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; } = [];
        public decimal Total => Items.Sum(i => i.Quantity * i.MenuItem.Price);
    }
}
