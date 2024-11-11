using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ProductConstants;
namespace DeskMarket.ViewModels;

public class ProductAddViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(MaxProductNameLength, MinimumLength = 2, ErrorMessage = ProductNameMaxLengthErrorMessage)]
    public string ProductName { get; set; } = null!;

    [Required]
    [StringLength(MaxDescriptionLength, MinimumLength = 10, ErrorMessage = DescriptionMaxLengthErrorMessage)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(MinPrice, MaxPrice, ErrorMessage = PriceRangeErrorMessage)]
    public decimal Price { get; set; }

    [Url(ErrorMessage = UrlErrorMessage)]
    public string? ImageUrl { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "dd-MM-yyyy}")]
    public DateTime AddedOn { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

    public bool IsDeleted { get; set; } = false;
}