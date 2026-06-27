namespace ECommerceBackend.EventTypes;

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
