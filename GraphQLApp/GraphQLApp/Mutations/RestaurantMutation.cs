using Domain.Entities;
using Domain.Interfaces.Services;
using GraphQL.GraphQLApp.Types;
using GraphQL.Types;

namespace GraphQL.GraphQLApp.Mutations;

public class RestaurantMutation : ObjectGraphType
{
    public RestaurantMutation(IUserService userService)
    {
        Field<UserType>(
            "createUser",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }),
            resolve: context =>
            {
                var user = context.GetArgument<User>("user");
                return userService.CreateUserAsync(user!);
            }
        );
    }
}