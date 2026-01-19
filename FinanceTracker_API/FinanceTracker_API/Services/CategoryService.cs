namespace FinanceTracker_API.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context) => _context = context;

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Icon = c.Icon,
                    Color = c.Color
                }).ToListAsync();
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var category = new Category 
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Icon = dto.Icon,
                Color = dto.Color
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto { Id = category.Id, Name = category.Name, Icon = category.Icon, Color = category.Color };
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
