using Domain.Entities;

namespace GraphQL.Schema;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<IdType>>();
        descriptor.Field(t => t.UserId).Type<IntType>();
        descriptor.Field(t => t.Items).Type<ListType<OrderItemType>>();
        descriptor.Field(t => t.Total).Type<DecimalType>();
    }
}