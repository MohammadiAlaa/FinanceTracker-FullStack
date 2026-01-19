namespace FinanceTracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryCreateDto dto)
        {
            var result = await _categoryService.CreateCategoryAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
