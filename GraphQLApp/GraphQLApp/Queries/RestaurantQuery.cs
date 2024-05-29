using Domain.Interfaces.Services;
using GraphQL.GraphQLApp.Types;
using GraphQL.Types;

namespace GraphQL.GraphQLApp.Queries;

public class RestaurantQuery : ObjectGraphType
{
    public RestaurantQuery(IUserService userService)
    {
        Field<ListGraphType<UserType>>(
            "users",
            resolve: context => userService.GetAllUsersAsync()
        );

        Field<UserType>(
            "user",
            arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
            resolve: context => userService.GetUserByIdAsync(context.GetArgument<int>("id"))
        );
    }
}