using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeskMarket.Common;

namespace DeskMarket.Data.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ProductConstants.MaxProductNameLength, ErrorMessage = ProductConstants.ProductNameMaxLengthErrorMessage)]
    public string ProductName { get; set; } = null!;

    [Required]
    [MaxLength(ProductConstants.MaxDescriptionLength, ErrorMessage = ProductConstants.DescriptionMaxLengthErrorMessage)]
    public string Description { get; set; } = null!;

    [Required]
    [Range((double)ProductConstants.MinPrice, (double)ProductConstants.MaxPrice, ErrorMessage = ProductConstants.PriceRangeErrorMessage)]
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