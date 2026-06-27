using ECommerceBackend.EventTypes;

public class UserAccountDeleted : Event
{
    public required string UserId { get; set; }
    public override string TopicName => "user_account_deleted";
}
