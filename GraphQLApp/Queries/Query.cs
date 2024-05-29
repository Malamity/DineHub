using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Queries;

public class Query
{

    [UseDbContext(typeof(ApplicationDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<MenuItem> GetMenuItems([ScopedService] ApplicationDbContext context) => context.MenuItems;

    [UseDbContext(typeof(ApplicationDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Order> GetOrders([ScopedService] ApplicationDbContext context) => context.Orders;

    [UseDbContext(typeof(ApplicationDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<OrderItem> GetOrderItems([ScopedService] ApplicationDbContext context) => context.OrderItems;

    [UseDbContext(typeof(ApplicationDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers([ScopedService] IDbContextFactory<ApplicationDbContext> context)
    {
        var dbContext = context.CreateDbContext();
        return dbContext.Users;
    }
}