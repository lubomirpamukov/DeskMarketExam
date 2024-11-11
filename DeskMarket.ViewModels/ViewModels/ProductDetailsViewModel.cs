namespace DeskMarket.ViewModels;

public class ProductDetailsViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string CategoryName { get; set; } = null!;
    public string Seller { get; set; } = null!;
    public DateTime AddedOn { get; set; }
    public bool HasBought { get; set; }
   
}
