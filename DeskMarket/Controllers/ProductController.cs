using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DeskMarket.Controllers;
[Authorize]
public class ProductController(ApplicationDbContext context, UserManager<IdentityUser> userManager) : Controller
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly ApplicationDbContext _dbContext = context;

    public async Task<IActionResult> Index() 
    {
        
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var viewModel = new ProductAddViewModel
        {
            Categories = await _dbContext.Categories.Select(c => new CategoryViewModel
            { 
                Id = c.Id,
                Name = c.Name
            }).ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductAddViewModel model) 
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await _dbContext.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToListAsync();

            return View(model);
        }


        var productToAdd = new Product 
        {
            ProductName = model.ProductName,
            Description = model.Description,
            Price = model.Price,
            ImageUrl = model.ImageUrl,
            SellerId = _userManager.GetUserId(User)!,
            AddedOn = model.AddedOn,
            CategoryId = model.CategoryId,
            IsDeleted = model.IsDeleted,
        };

        await _dbContext.Products.AddAsync(productToAdd);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("All");
    }
}
