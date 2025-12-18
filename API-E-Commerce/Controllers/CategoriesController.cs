using API_E_Commerce.DTO;
using API_E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_E_Commerce.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [EndpointSummary("Get all categories")]
    [EndpointDescription("Retrieves a list of all categories in the e-commerce platform.")]
    public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
    {
        List<CategoryDto> categories = await _categoryService.GetAllCategories();
            
        return Ok(categories);
    }

    [HttpGet("{idCategory}")]
    [EndpointSummary("Get category by ID")]
    [EndpointDescription("Retrieves a specific category by its unique identifier.")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(int idCategory)
    {
        CategoryDto? category = await _categoryService.GetCategoryById(idCategory);
        
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpGet("search/{query}")]
    [EndpointSummary("Get categories by name")]
    [EndpointDescription("Retrieves a list of categories that match the specified name.")]
    public async Task<ActionResult<List<CategoryDto>>> GetCategoriesByName(string query)
    {
        List<CategoryDto> categories = await _categoryService.GetCategoriesByName(query);
        return Ok(categories);
    }

    [HttpPost]
    [EndpointSummary("Create a new category")]
    [EndpointDescription("Creates a new category in the e-commerce platform.")]
    public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto categoryDto)
    {
        CategoryDto createdCategory = await _categoryService.CreateCategory(categoryDto);
        return CreatedAtAction(nameof(GetCategoryById), new { idCategory = createdCategory.Id }, createdCategory);
    }
}