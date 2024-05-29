using Domain.Interfaces.Services;
using GraphQL.GraphQLApp.Mutations;
using GraphQL.GraphQLApp.Queries;
using Schema = GraphQL.Types.Schema;

namespace GraphQL.GraphQLApp.Schemas;

public class RestaurantSchema : Schema
{
    public RestaurantSchema(IUserService userService, IServiceProvider provider)
        : base(provider)
    {
        Query = new RestaurantQuery(userService);
        Mutation = new RestaurantMutation(userService);
    }
}