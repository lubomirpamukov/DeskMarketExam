using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.CategoryConstants;
namespace DeskMarket.ViewModels;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required]
    [MinLength(MinNameLength, ErrorMessage = NameLengthErrorMessage)]
    [MaxLength(MaxNameLength, ErrorMessage = NameMaxLengthErrorMessage)]
    public string Name { get; set; } = null!;
}

