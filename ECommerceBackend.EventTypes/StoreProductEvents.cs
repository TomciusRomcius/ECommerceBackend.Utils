namespace ECommerceBackend.EventTypes;

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