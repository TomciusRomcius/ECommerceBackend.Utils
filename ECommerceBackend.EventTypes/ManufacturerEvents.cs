namespace ECommerceBackend.EventTypes;

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