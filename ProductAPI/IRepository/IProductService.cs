using ProductAPI.DTO;

namespace ProductAPI.IRepository
{
    public interface IProductService
    {
        Task<bool> CreateProduct(CreateProductRequest request);
    }
}
