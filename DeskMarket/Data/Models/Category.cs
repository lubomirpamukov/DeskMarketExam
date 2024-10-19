using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "Category name must be at least 3 characters long.")]
    [MaxLength(20, ErrorMessage = "Category name cannot exceed 20 characters.")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
