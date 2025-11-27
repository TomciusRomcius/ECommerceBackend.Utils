using System.ComponentModel.DataAnnotations;

namespace ECommerceBackend.Utils.Database;

public class MicroserviceHosts 
{
    [Required]
    public required string UserServiceUrl { get; set; }
    [Required]
    public required string ProductServiceUrl { get; set; }
    [Required]
    public required string StoreServiceUrl { get; set; }
    [Required]
    public required string PaymentServiceUrl { get; set; }
    [Required]
    public required string OrderServiceUrl { get; set; }
}