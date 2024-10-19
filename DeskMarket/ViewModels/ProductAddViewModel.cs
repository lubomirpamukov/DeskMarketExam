using System.ComponentModel.DataAnnotations;

namespace DeskMarket.ViewModels;

public class ProductAddViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 60 characters.")]
    public string ProductName { get; set; } = null!;

    [Required]
    [StringLength(250, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 250 characters.")]
    public string Description { get; set; } = null!;

    [Required]
    [Range(1.00, 3000.00, ErrorMessage = "Price must be between 1.00 and 3000.00.")]
    public decimal Price { get; set; }

    [Url(ErrorMessage = "Please enter a valid URL.")]
    public string? ImageUrl { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
    public DateTime AddedOn { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

    public bool IsDeleted { get; set; } = false;
}


//To do make magic string to global constants