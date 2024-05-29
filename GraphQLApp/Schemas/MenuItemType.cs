using Domain.Entities;

namespace GraphQL.Schema;

public class MenuItemType : ObjectType<MenuItem>
{
    protected override void Configure(IObjectTypeDescriptor<MenuItem> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.Name).Type<StringType>();
        descriptor.Field(t => t.Description).Type<StringType>();
        descriptor.Field(t => t.Price).Type<DecimalType>();
        descriptor.Field(t => t.Category).Type<StringType>();
    }
}