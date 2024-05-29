using Domain.Entities;

namespace GraphQL.Schema;

public class OrderItemType : ObjectType<OrderItem>
{
    protected override void Configure(IObjectTypeDescriptor<OrderItem> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.OrderId).Type<IntType>();
        descriptor.Field(t => t.MenuItemId).Type<IntType>();
        descriptor.Field(t => t.Quantity).Type<IntType>();
        descriptor.Field(t => t.MenuItem).Type<MenuItemType>();
    }
}