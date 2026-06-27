namespace ECommerceBackend.EventTypes;

public class ProductAddedToCartEvent : Event
{
    public required string UserId { get; set; }
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
    public required int Quantity { get; set; }
    public override string TopicName => "product_added_to_cart";
}

public class ProductCartQuantityModifiedEvent : Event
{
    public required string UserId { get; set; }
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
    public required int Quantity { get; set; }
    public override string TopicName => "product_cart_quantity_modified";
}

public class ProductRemovedFromCartEvent : Event
{
    public required string UserId { get; set; }
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
    public override string TopicName => "product_removed_from_cart";
}

public class UserCartClearedEvent : Event
{
    public required string UserId { get; set; }
    public override string TopicName => "user_cart_cleared";
}
