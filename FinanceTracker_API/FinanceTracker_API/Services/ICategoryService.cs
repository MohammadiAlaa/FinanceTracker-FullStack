namespace FinanceTracker_API.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto); 
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
