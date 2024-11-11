using Microsoft.AspNetCore.Identity;

namespace DeskMarket.Data.Models;

public class ProductClient
{
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;

    public string ClientId { get; set; } = null!;
    public virtual ApplicationUser Client { get; set; } = null!;
}
