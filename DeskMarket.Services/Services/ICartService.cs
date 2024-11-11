using DeskMarket.ViewModels;
using DeskMarket.Common.Enums;

namespace DeskMarket.Services;

public interface ICartService
{
    public Task<IEnumerable<CartItemViewModel>> GetCartItemsAsync(string userId);

    public Task<AddToCartResult> AddToCartAsync(int productId, string userId);

    public Task<bool> RemoveProductFromCartAsync(int productId, string userId);
}
