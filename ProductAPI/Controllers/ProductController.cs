using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTO.Request;
using ProductAPI.Entity;
using ProductAPI.IRepository;
using ProductAPI.Repository;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var result = await _service.CreateProduct(request);

            if(result.IsSuccessful == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("get-products")]
        public async Task<IActionResult> GetProducts()
        {           
            return Ok(await _service.GetProducts());            
        }

        [HttpPut]
        [Route("update-product/{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] UpdateProductRequest request)
        {
            var result = await _service.UpdateProduct(id, request);

            if (result.IsSuccessful == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("delete-product/{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var result = await _service.DeleteProduct(id);

            if (result.IsSuccessful == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
