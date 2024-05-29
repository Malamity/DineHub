using Domain.Entities;
using GraphQL.Types;

namespace GraphQL.GraphQLApp.Types;

public sealed class UserInputType : InputObjectGraphType<User>
{
    public UserInputType()
    {
        Name = "UserInput";
        Field(x => x.Username);
        Field(x => x.PasswordHash);
        Field(x => x.Role);
    }
}