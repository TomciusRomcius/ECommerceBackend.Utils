namespace ECommerceBackend.EventTypes;

public abstract class Event
{
    public Guid MessageId { get; set; } = Guid.NewGuid();
}

public class ChargeSucceededEvent : Event
{
    public required string UserId { get; set; }
    public required string OrderId { get; set; }
    public required int Amount { get; set; }
}

// Product service
public class ProductCreatedEvent : Event
{
    public required int ProductId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}

public class ProductUpdatedEvent : Event
{
    public required int ProductId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}


public class ProductDeletedEvent : Event
{
    public required int ProductId { get; set; }
}

// Store service
public class ProductAddedToStoreEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
    public required int Stock { get; set; }
}

public class ProductStockUpdatedEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
    public required int Stock { get; set; }
}

public class ProductRemovedFromStoreEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required int ProductId { get; set; }
}

public class StoreCreatedEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required string DisplayName { get; set; }
    public required string Address { get; set; }
}

public class StoreUpdatedEvent : Event
{
    public required int StoreLocationId { get; set; }
    public required string DisplayName { get; set; }
    public required string Address { get; set; }
}

public class StoreDeletedEvent : Event
{
    public required int StoreLocationId { get; set; }
}

public class PaymentFailedEvent : Event
{
    public required string UserId { get; set; }
    public required string OrderId { get; set; }
}

public class UserAccountDeleted : Event
{
    public required string UserId { get; set; }
}

