using GraphQL.Queries;
using GraphQL.Schema;

namespace GraphQL.Extensions;

public static class GraphQlServiceExtensions
{
    public static IServiceCollection AddGraphQlServices(this IServiceCollection services)
    {
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