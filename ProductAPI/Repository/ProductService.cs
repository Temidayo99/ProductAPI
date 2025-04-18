using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ProductAPI.DatabaseContext;
using ProductAPI.DTO.GenericDto;
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

        public async Task<BaseResponse<bool>> CreateProduct(CreateProductRequest request)
        {
            try
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

                    return new BaseResponse<bool>(true, "00", "Product registration successful", true);
                }

                return new BaseResponse<bool>(false, "01", "Product registration failed", false);
            }
            catch (Exception ex)
            {
                //log the exception
                return new BaseResponse<bool>(false, "99", "Something went wrong. Please try again later", false);
            }
        }

        public async Task<BaseResponse<List<ProductResponse>>> GetProducts()
        {
            try
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

                if (products.Any())
                {
                    return new BaseResponse<List<ProductResponse>>(products, "04", "Successful", true);
                }

                return new BaseResponse<List<ProductResponse>>(null, "04", "No record found", false);
            }
            catch (Exception ex) 
            {
                return new BaseResponse<List<ProductResponse>>(null, "99", "Something went wrong. Please try again later", false);
            }
        }

        public async Task<BaseResponse<bool>> UpdateProduct(long id, UpdateProductRequest request)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {
                    return new BaseResponse<bool>(false, "02", "Product update failed", false);
                }

                product.Price = request.Price;
                product.Quantity = request.Quantity;
                product.UpdatedAt = request.UpdatedAt;
                product.IsActive = request.IsActive;


                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return new BaseResponse<bool>(true, "03", "Product updated successfully", true);
            }
            catch (Exception ex) 
            {
                return new BaseResponse<bool>(false, "99", "Something went wrong. Please try again later", false);
            }
        }

        public async Task<BaseResponse<bool>> DeleteProduct(long id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {
                    return new BaseResponse<bool>(false, "01", "failed", false);
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return new BaseResponse<bool>(true, "01", "Product deleted failed", true);
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>(false, "99", "Something went wrong. Please try again later", false);
            }
        }
    }
}
