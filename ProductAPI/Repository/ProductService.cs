using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ProductAPI.DatabaseContext;
using ProductAPI.DTO.Request;
using ProductAPI.DTO.Response;
using ProductAPI.Entity;
using ProductAPI.IRepository;

namespace ProductAPI.Repository
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProduct(CreateProductRequest request)
        {
            var nameExist = await _context.Products.FirstOrDefaultAsync(x => x.Name == request.Name);

            if (nameExist == null) 
            {
                var newProduct = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Quantity = request.Quantity,
                    Category = request.Category,
                    ImageUrl = request.ImageUrl,
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    IsActive = request.IsActive
                };

                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            

            return false;
        }

        public async Task<List<ProductResponse>> GetProducts()
        {
            var products = await _context.Products.Select(x => new ProductResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Quantity = x.Quantity,
                Category = x.Category,
                ImageUrl = x.ImageUrl,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                IsActive = x.IsActive
            }).ToListAsync();

            return products;
        }

        public async Task<bool> UpdateProduct(long id, UpdateProductRequest request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                
            if (product == null)
            {
                return false;
            }

            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.UpdatedAt = request.UpdatedAt;
            product.IsActive = request.IsActive;
            

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(long id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
