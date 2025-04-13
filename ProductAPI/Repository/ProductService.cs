using Microsoft.EntityFrameworkCore;
using ProductAPI.DatabaseContext;
using ProductAPI.DTO;
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
    }
}
