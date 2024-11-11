using DeskMarket.Data.Models;
using DeskMarket.ViewModels;

namespace DeskMarket.Services
{
    public interface IProductService
    {
        public Task<bool> AddProduct(ProductAddViewModel model, string sellerId);

        public Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string clientId);

        public  Task<ProductEditViewModel> EditProductsAsync(int id, string sellerId);

        public  Task<bool> UpdateProductAsync(ProductEditViewModel model, string userId);

        public Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

        public Task<DeleteProductViewModel> GetProductForDeletionAsync(int productId);

        public Task<bool> DeleteProductAsync(int productId, string userId);

        public Task<ProductDetailsViewModel> GetProductDetailsAsync(int productId, string clientId);
    }
}
