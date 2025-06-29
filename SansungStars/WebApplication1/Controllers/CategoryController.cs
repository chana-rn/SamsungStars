using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
////delete comments from all files
//clean code - use meaningful function names in all files

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860//

namespace SamsungStars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoryController>//
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            List<CategoryDTO> categories = await _categoryService.getCategory();
            if (categories == null)
                return NotFound();
            return Ok(categories);
            //return categories == null ? NotFound() : Ok(categories);
        }
    }
}
