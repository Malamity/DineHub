using Domain.Entities;
using Domain.Enums;

namespace GraphQL.Schema;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Username).Type<StringType>();
        descriptor.Field(t => t.PasswordHash).Ignore();
        descriptor.Field(t => t.Role).Type<EnumType<Role>>();
    }
}