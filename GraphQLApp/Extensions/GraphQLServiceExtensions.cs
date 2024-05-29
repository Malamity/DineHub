using GraphQL.GraphQLApp.Mutations;
using GraphQL.GraphQLApp.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Extensions;

public static class GraphQLServiceExtensions
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<RestaurantQuery>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<RestaurantMutation>();

        return services;
    }
}