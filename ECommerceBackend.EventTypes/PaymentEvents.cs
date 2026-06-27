namespace ECommerceBackend.EventTypes;

public class CheckoutSucceededEvent : Event
{
    public required string UserId { get; set; }
    public required string OrderId { get; set; }
    public required int Amount { get; set; }
    public override string TopicName => "checkout_succeeded";
}

public class CheckoutPaymentFailedEvent : Event
{
    public required string UserId { get; set; }
    public required string OrderId { get; set; }
    public override string TopicName => "checkout_payment_failed";
}

public class CheckoutSessionExpiredEvent : Event
{
    public required string UserId { get; set; }
    public required string OrderId { get; set; }
    public override string TopicName => "checkout_session_expired";
}
