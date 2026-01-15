namespace ECommerceBackend.EventTypes;

public class ChargeSucceededEvent
{
    public required string UserId { get; set; }
    public required int Amount { get; set; }
}

public class UserAccountDeleted
{
    public required string UserId { get; set; }
}