using Domain.Entities;
using GraphQL.Types;

namespace GraphQL.GraphQLApp.Types;

public sealed class UserType : ObjectGraphType<User>
{
    public UserType()
    {
        Name = "User";
        Field(x => x.Id).Description("User identifier.");
        Field(x => x.Username).Description("Username of the user.");
        Field(x => x.Role).Description("Role of the user.");
    }
}