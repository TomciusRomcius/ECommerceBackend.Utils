namespace ECommerceBackend.EventTypes;

public abstract class Event
{
    public Guid MessageId { get; } = Guid.NewGuid();
    public abstract string TopicName { get; }
}