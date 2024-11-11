using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> DeleteProductAsync(int productId, string userId)
        {
            var product = await _dbContext.Products
            .Where(p => p.Id == productId && p.SellerId == userId)
            .FirstOrDefaultAsync();

            if (product == null)
            {
                return false;
            }

            product.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
            
        }

        public async Task<DeleteProductViewModel> GetProductForDeletionAsync(int productId)
        {
             var product = await _dbContext.Products
            .Where(p => p.Id == productId)
            .Include(prod => prod.Seller)
            .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var viewModel = new DeleteProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Seller = product.Seller.UserName!,
                SellerId = product.SellerId,
            };

            return viewModel;
        }

        public async Task<ProductEditViewModel> EditProductsAsync(int id, string userId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            else if (product.SellerId != userId) 
            {
                throw new ArgumentException("User is not the seller");
            }
            // random comment to check git problem
            return new ProductEditViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                AddedOn = product.AddedOn,
                CategoryId = product.CategoryId,
                IsDeleted = product.IsDeleted,
                SellerId = product.SellerId!,
                Categories = await GetCategoriesAsync(),
            };
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string clientId)
        {
            

            var products = await _dbContext.Products
                .Where(p => p.IsDeleted == false)
                .Select(p => new
                {
                    p.Id,
                    p.ProductName,
                    p.Price,
                    p.ImageUrl,
                    IsSeller = (clientId != null && clientId == p.SellerId),
                    HasBought = _dbContext.ProductsClients
                        .Any(pc => pc.ClientId == clientId && pc.ProductId == p.Id)
                })
                .ToListAsync();

            return  products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                IsSeller = p.IsSeller,
                HasBought = p.HasBought
            }).ToList();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            return  await _dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }


        public async Task<bool> UpdateProductAsync(ProductEditViewModel model, string userId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null || product.SellerId != userId)
            {
                return false;
            }

            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.AddedOn = model.AddedOn;
            product.CategoryId = model.CategoryId;
            product.IsDeleted = model.IsDeleted;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async  Task<ProductDetailsViewModel> GetProductDetailsAsync(int productId, string clientId)
        {
              var product = await _dbContext.Products
              .Include(p => p.Category)
              .Include(p => p.Seller)
              .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            bool hasBoughtProduct = await _dbContext.ProductsClients
                .AnyAsync(pc => pc.ProductId == productId && pc.ClientId == clientId);

            var productView = new ProductDetailsViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryName = product.Category.Name,
                Seller = product.Seller.UserName!,
                AddedOn = product.AddedOn,
                HasBought = hasBoughtProduct

            };

            return productView;
        }

        public async Task<bool> AddProduct(ProductAddViewModel model, string sellerId)
        {
            if (model == null) 
            {
                return false;
            }

            var categoryExists = await _dbContext.Categories.AnyAsync(c => c.Id == model.CategoryId);
            if (!categoryExists) 
            {
                return false;
            }

            var productToAdd = new Product
            {
                ProductName = model.ProductName,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                SellerId = sellerId,
                AddedOn = model.AddedOn,
                CategoryId = model.CategoryId,
                IsDeleted = model.IsDeleted,
            };

            await _dbContext.Products.AddAsync(productToAdd);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
