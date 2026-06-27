namespace ECommerceBackend.EventTypes;

public class ProductImageCreatedEvent : Event
{
    public required int ProductImageId { get; set; }
    public required int ProductId { get; set; }
    public required string S3Key { get; set; }
    public override string TopicName => "product_image_created";
}

public class ProductImageUpdatedEvent : Event
{
    public required int ProductImageId { get; set; }
    public required int ProductId { get; set; }
    public required string S3Key { get; set; }
    public override string TopicName => "product_image_updated";
}

public class ProductImageDeletedEvent : Event
{
    public required int ProductImageId { get; set; }
    public required int ProductId { get; set; }
    public override string TopicName => "product_image_deleted";
}