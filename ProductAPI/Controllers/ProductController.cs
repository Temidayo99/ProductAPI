using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTO;
using ProductAPI.Repository;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
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
                return BadRequest("Failed");
            }
        }
    }
}
