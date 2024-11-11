using DeskMarket.Data;
using DeskMarket.Common.Enums;
using DeskMarket.Data.Models;
using DeskMarket.Services;
using DeskMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DeskMarket.Controllers;
[Authorize]
public class ProductController
    (
    UserManager<ApplicationUser> userManager, 
    IProductService productService,
    ICartService cartService
    ) : Controller
{
    private readonly IProductService _productService = productService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ICartService _cartService = cartService;

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var clientId = _userManager.GetUserId(User);

        if (clientId == null) 
        {
            return NotFound();
        }

        var productViewModel = await _productService.GetAllProductsAsync(clientId);

        return View(productViewModel);
    }


    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var viewModel = new ProductAddViewModel
        {
            Categories = await _productService.GetCategoriesAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductAddViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await _productService.GetCategoriesAsync();

            return View(model);
        }

        var sellerId = _userManager.GetUserId(User);

        if (sellerId == null) 
        {
            return NotFound();
        }

        if (!await _productService.AddProduct(model, sellerId)) 
        {
            return BadRequest();
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Cart()
    {
        var clientId = _userManager.GetUserId(User);

        if (clientId == null)
        {
            return NotFound();
        }

        return View(await _cartService.GetCartItemsAsync(clientId));
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var userId = _userManager?.GetUserId(User);

        if (userId == null) 
        {
            return NotFound();
        }
        var productView = await _productService.GetProductDetailsAsync(id, userId);

        if (productView == null) 
        {
            return NotFound();
        }
       
        return View(productView);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int id)
    {
        var clientId = _userManager.GetUserId(User);

        if (clientId == null)
        {
            return NotFound();
        }

        var result = await _cartService.AddToCartAsync(id, clientId);

        switch (result) 
        {
            case AddToCartResult.Success:
                return RedirectToAction("Index");
            case AddToCartResult.AlreadyExists:
                return RedirectToAction("Cart");
            case AddToCartResult.UserNotFound:
                return NotFound();
            case AddToCartResult.ProductNotFound:
                return BadRequest("Product not found");
            default:
                return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var clientId = _userManager.GetUserId(User);

        if (clientId == null) 
        {
            return NotFound();
        }

        var result = await _cartService.RemoveProductFromCartAsync(id, clientId);

        if (!result) 
        {
            return RedirectToAction("Cart");
        }

        return RedirectToAction("Cart");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var viewModel = await _productService.GetProductForDeletionAsync(id);

        if (viewModel == null) 
        {
            return NotFound();
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteProductViewModel model)
    {
        var userId = _userManager.GetUserId(User);

        if (userId == null) 
        {
            return NotFound();
        }

        var productId = model.Id;

        if (await _productService.DeleteProductAsync(productId, userId!)) 
        {
            return RedirectToAction("Index");
        }

        return NotFound();

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        var productResult = await _productService.EditProductsAsync(id, userId!);

        if (productResult == null)
        {
            return NotFound();
        }

        return View(productResult);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await _productService.GetCategoriesAsync();

            return View(model);
        }

        var userId = _userManager.GetUserId(User);

        if (await _productService.UpdateProductAsync(model, userId!))
        {
            return RedirectToAction("Details", new { id = model.Id });
        }

        return NotFound();
    }
}
