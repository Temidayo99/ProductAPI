using ProductAPI.DTO.Request;
using ProductAPI.DTO.Response;

namespace ProductAPI.IRepository
{
    public interface IProductService
    {
        Task<bool> CreateProduct(CreateProductRequest request);

        Task<List<ProductResponse>> GetProducts();

        Task<bool> UpdateProduct(long id, UpdateProductRequest request);

        Task<bool> DeleteProduct(long id);
    }
}
