using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskMarket.Data.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(60)]
    public string ProductName { get; set; } = null!;

    [Required]
    [MaxLength(250)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(1.00, 3000.00)]
    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public string SellerId { get; set; } = null!;

    [Required]
    public virtual IdentityUser Seller { get; set; } = null!;

    [Required]
    public DateTime AddedOn { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<ProductClient> ProductsClients { get; set; } = new List<ProductClient>();
}
//Todo make constants