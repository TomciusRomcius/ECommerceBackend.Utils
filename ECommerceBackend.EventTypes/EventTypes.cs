namespace ECommerceBackend.EventTypes;

public abstract class Event
{
    public Guid MessageId { get; } = Guid.NewGuid();
    public abstract string TopicName { get; }
}

public class ChargeSucceededEvent : Event
{
    public required string UserId { get; set; }
    public required string OrderId { get; set; }
    public required long Amount { get; set; }
    public override string TopicName => "charge_succeeded";
}

// Product service
public class ProductCreatedEvent : Event
{
    public required int ProductId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int ManufacturerId { get; set; }
    public required int CategoryId { get; set; }
    public override string TopicName => "product_created";
}

public class ProductUpdatedEvent : Event
{
    public required int ProductId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required int ManufacturerId { get; set; }
    public required int CategoryId { get; set; }
    public override string TopicName => "product_updated";
}


public class ProductDeletedEvent : Event
{
    public required int ProductId { get; set; }
    public override string TopicName => "product_deleted";
}

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

// Store service
public class ProductAddedToStoreEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
    public required int Stock { get; set; }
    public override string TopicName => "product_added_to_store";
}

public class ProductStockUpdatedEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
    public required int Stock { get; set; }
    public override string TopicName => "product_stock_updated";
}

public class ProductRemovedFromStoreEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
    public override string TopicName => "product_removed_from_store";
}

public class StoreCreatedEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required string DisplayName { get; set; }
    public required string Address { get; set; }
    public override string TopicName => "store_created";
}

public class StoreUpdatedEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required string DisplayName { get; set; }
    public required string Address { get; set; }
    public override string TopicName => "store_updated";
}

public class StoreDeletedEvent : Event
{
    public required int StoreLocationId { get; set; }
    public override string TopicName => "store_deleted";
}

public class PaymentFailedEvent : Event
{
    public required string UserId { get; set; }
    public required string OrderId { get; set; }
    public override string TopicName => "payment_failed";
}

public class UserAccountDeleted : Event
{
    public required string UserId { get; set; }
    public override string TopicName => "user_account_deleted";
}

public class ManufacturerCreatedEvent : Event
{
    public required int ManufacturerId { get; set; }
    public required string Name { get; set; }
    public override string TopicName => "manufacturer_created";
}

public class ManufacturerUpdatedEvent : Event
{
    public required int ManufacturerId { get; set; }
    public required string Name { get; set; }
    public override string TopicName => "manufacturer_updated";
}

public class ManufacturerDeletedEvent : Event
{
    public required int ManufacturerId { get; set; }
    public override string TopicName => "manufacturer_deleted";
}

public class CategoryCreatedEvent : Event
{
    public required int CategoryId { get; set; }
    public required string Name { get; set; }
    public override string TopicName => "category_created";
}

public class CategoryUpdatedEvent : Event
{
    public required int CategoryId { get; set; }
    public required string Name { get; set; }
    public override string TopicName => "category_updated";
}

public class CategoryDeletedEvent : Event
{
    public required int CategoryId { get; set; }
    public override string TopicName => "category_deleted";
}

public class ProductImageCreatedEvent : Event
{
    public required int ProductImageId { get; set; }
    public required int ProductId { get; set; }
    public required string S3Key { get; set; }
    public override string TopicName => "product_image_created";
}

public class ProductImageUpdatedEvent : Event
{
    public required int ProductImageId { get; set; }
    public required int ProductId { get; set; }
    public required string S3Key { get; set; }
    public override string TopicName => "product_image_updated";
}

public class ProductImageDeletedEvent : Event
{
    public required int ProductImageId { get; set; }
    public required int ProductId { get; set; }
    public override string TopicName => "product_image_deleted";
}