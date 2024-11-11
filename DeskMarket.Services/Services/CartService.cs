using DeskMarket.Common.Enums;
using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Services;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _dbContext;
    public CartService(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async  Task<AddToCartResult> AddToCartAsync(int productId, string clientId)
    {
        bool productExist = await _dbContext.Products.AnyAsync(p => p.Id == productId);

        if (!productExist)
        {
            return AddToCartResult.ProductNotFound;
        }

        bool clientExist = await _dbContext.Users.AnyAsync(c => c.Id == clientId);

        if (!clientExist) 
        {
            return AddToCartResult.UserNotFound;
        }

        var existingCartItem = await _dbContext.ProductsClients
            .AnyAsync(pc => pc.ClientId == clientId && pc.ProductId == productId);

        if (existingCartItem)
        {
            return AddToCartResult.AlreadyExists;
        }
        else
        {
            var productClient = new ProductClient
            {
                ClientId = clientId,
                ProductId = productId
            };

            await _dbContext.ProductsClients.AddAsync(productClient);
            await _dbContext.SaveChangesAsync();
            return AddToCartResult.Success;
        }
    }

    public async Task<IEnumerable<CartItemViewModel>> GetCartItemsAsync(string userId)
    {
        var clientCart = await _dbContext.ProductsClients
            .Where(pc => pc.ClientId == userId)
            .Include(p => p.Product)
            .Where(p => p.Product.IsDeleted == false)
            .Select(p => new CartItemViewModel
            {
                Id = p.ProductId,
                ProductName = p.Product.ProductName,
                Price = p.Product.Price,
                ImageUrl = p.Product.ImageUrl,
            })
            .ToListAsync();

        return clientCart;
    }

    public async Task<bool> RemoveProductFromCartAsync(int productId, string userId)
    {
        var productExist = await _dbContext.Products.AnyAsync(p => p.Id == productId);

        var userExist = await _dbContext.Users.AnyAsync(u => u.Id == userId);

        if (!userExist || !productExist) 
        {
            return false;
        }

        var productToRemove = await _dbContext.ProductsClients
            .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.ClientId == userId);

        if (productToRemove == null)
        {
            return false;
        }

        _dbContext.ProductsClients.Remove(productToRemove);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
