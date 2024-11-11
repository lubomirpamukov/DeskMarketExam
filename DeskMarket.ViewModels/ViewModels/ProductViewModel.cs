﻿namespace DeskMarket.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; } 
    public string? ImageUrl { get; set; } 
    public bool IsSeller { get; set; } 
    public bool HasBought { get; set; } 
}
