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

            if(result == true)
            {
                return Ok("Product Created Successfully");
            }
            else
            {
                return BadRequest("Failed - Product exists already");
            }
        }

        [HttpGet]
        [Route("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _service.GetProducts();

            if (products != null)
            {
                return Ok(products);
            }
            else
            {
                return NotFound(products);
            }
        }

        [HttpPut]
        [Route("update-product/{id:long}")]
        public async Task<IActionResult> UpdateProduct(long id, [FromBody] UpdateProductRequest request)
        {
            var product = await _service.UpdateProduct(id, request);

            if (product == true)
            {
                return Ok("Product Updated Successfully");
            }
            else
            {
                return BadRequest("Product Update Failed");
            }
        }

        [HttpDelete]
        [Route("delete-product/{id:long}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _service.DeleteProduct(id);

            if (product == true)
            {
                return Ok("Product Deleted Successfully");
            }
            else
            {
                return BadRequest("Unable to delete product");
            }
        }
    }
}
