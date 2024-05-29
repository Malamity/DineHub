using GraphQL.Queries;
using GraphQL.Schema;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Extensions;

public static class GraphQlServiceExtensions
{
    public static IServiceCollection AddGraphQlServices(this IServiceCollection services)
    {
        services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("RestaurantDb"));

        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddType<MenuItemType>()
            .AddType<OrderType>()
            .AddType<OrderItemType>()
            .AddType<UserType>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}