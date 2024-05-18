namespace Domain.Entities

{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal Total => Items.Sum(i => i.Quantity * i.MenuItem.Price);
    }
}
