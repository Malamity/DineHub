using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            if (context.MenuItems.Any() && context.Users.Any() && context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            var users = new List<User>
            {
                new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("adminpassword"), Role = Role.Admin },
                new User { Username = "user1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password1"), Role = Role.User },
                new User { Username = "user2", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password2"), Role = Role.User }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            var menuItems = new List<MenuItem>
            {
                new MenuItem { Name = "Margherita Pizza", Description = "Classic Margherita with fresh mozzarella and basil", Price = 8.99m, Category = "Pizza" },
                new MenuItem { Name = "Pepperoni Pizza", Description = "Pepperoni with mozzarella and tomato sauce", Price = 9.99m, Category = "Pizza" },
                new MenuItem { Name = "Caesar Salad", Description = "Romaine lettuce with Caesar dressing and croutons", Price = 7.99m, Category = "Salad" },
                new MenuItem { Name = "Chocolate Cake", Description = "Rich chocolate cake with a molten center", Price = 5.99m, Category = "Dessert" }
            };

            context.MenuItems.AddRange(menuItems);
            await context.SaveChangesAsync();

            var orders = new List<Order>
            {
                new Order
                {
                    UserId = users[1].Id,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { MenuItemId = menuItems[0].Id, Quantity = 1 },
                        new OrderItem { MenuItemId = menuItems[2].Id, Quantity = 2 }
                    }
                },
                new Order
                {
                    UserId = users[2].Id,
                    Items = new List<OrderItem>
                    {
                        new OrderItem { MenuItemId = menuItems[1].Id, Quantity = 1 },
                        new OrderItem { MenuItemId = menuItems[3].Id, Quantity = 1 }
                    }
                }
            };

            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();
        }
    }
}
