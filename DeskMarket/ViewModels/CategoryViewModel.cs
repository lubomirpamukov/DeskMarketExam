using System.ComponentModel.DataAnnotations;

namespace DeskMarket.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 20 characters.")]
    public string Name { get; set; } = null!;
}

//To do make magic string to global constants