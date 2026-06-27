namespace ECommerceBackend.EventTypes;

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
