namespace ECommerceBackend.EventTypes;

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