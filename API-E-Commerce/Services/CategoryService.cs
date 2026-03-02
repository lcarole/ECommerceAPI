using API_E_Commerce.Contexts;
using API_E_Commerce.DTO;
using API_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace API_E_Commerce.Services;

public class CategoryService
{
    private readonly ECommerceContext _context;
    
    public CategoryService(ECommerceContext context)
    {
        _context = context;
    }
    
    public async Task<List<CategoryDto>> GetAllCategories()
    {
        List<CategoryDto> categories = await _context.Categories
            .Select(c => new CategoryDto(c))
            .ToListAsync();

        return categories;
    }

    public async Task<CategoryDto?> GetCategoryById(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        CategoryDto? categoryDto = category != null ? new CategoryDto(category) : null;

        return categoryDto;
    }

    public async Task<List<CategoryDto>> GetCategoriesByName(string name)
    {
        List<CategoryDto> categories = await _context.Categories
            .Where(c => EF.Functions.Like(c.Name.ToLower(), $"%{name.ToLower()}%"))
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto(c))
            .ToListAsync();

        return categories;
    }

    public async Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        Category category = new()
        {
            Name = createCategoryDto.Name,
            Description = createCategoryDto.Description
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return new CategoryDto(category);
    }
}