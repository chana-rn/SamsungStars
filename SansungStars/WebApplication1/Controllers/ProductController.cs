using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamsungStars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
       
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get(
            [FromQuery] string? description,
            [FromQuery] int? minPrice,
            [FromQuery] int? maxPrice,
            [FromQuery] int?[] categoryIds)
        {

            List<ProductDTO> products = await _productService.getProducts(description, minPrice, maxPrice, categoryIds);
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        
    }
}
