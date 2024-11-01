using DeskMarket.Common;
using System.ComponentModel.DataAnnotations;

namespace DeskMarket.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(CategoryConstants.MaxNameLength, ErrorMessage = CategoryConstants.NameMaxLengthErrorMessage)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
