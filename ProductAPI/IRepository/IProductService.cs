using ProductAPI.DTO.GenericDto;
using ProductAPI.DTO.Request;
using ProductAPI.DTO.Response;

namespace ProductAPI.IRepository
{
    public interface IProductService
    {
        Task<BaseResponse<bool>> CreateProduct(CreateProductRequest request);

        Task<BaseResponse<List<ProductResponse>>> GetProducts();

        Task<BaseResponse<bool>> UpdateProduct(long id, UpdateProductRequest request);

        Task<BaseResponse<bool>> DeleteProduct(long id);
    }
}
