using API_E_Commerce.DTO;
using API_E_Commerce.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_E_Commerce.Endpoints;

public static class CategoriesEndpoints
{
    public static RouteGroupBuilder MapCategoriesEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllCategories)
            .WithSummary("Get all categories")
            .WithDescription("Retrieves a list of all categories in the e-commerce platform.");

        group.MapGet("/{idCategory:int}", GetCategoryById)
            .WithName("GetCategoryById")
            .WithSummary("Get category by ID")
            .WithDescription("Retrieves a specific category by its unique identifier.")
            .Produces<CategoryDto>(200)
            .Produces(404);

        group.MapGet("/search", GetCategoriesByName)
            .WithSummary("Get categories by name")
            .WithDescription("Retrieves a list of categories that match the specified name.");

        group.MapPost("/", CreateCategory)
            .WithSummary("Create a new category")
            .WithDescription("Creates a new category in the e-commerce platform.")
            .Produces<CategoryDto>(201);

        return group;
    }

    private static async Task<Ok<List<CategoryDto>>> GetAllCategories(CategoryService categoryService)
    {
        List<CategoryDto> categories = await categoryService.GetAllCategories();
        
        return TypedResults.Ok(categories);
    }

    private static async Task<Results<Ok<CategoryDto>, NotFound>> GetCategoryById(int idCategory, CategoryService categoryService)
    {
        CategoryDto? category = await categoryService.GetCategoryById(idCategory);
        
        return category == null ? TypedResults.NotFound() : TypedResults.Ok(category);
    }

    private static async Task<Ok<List<CategoryDto>>> GetCategoriesByName(string query, CategoryService categoryService)
    {
        List<CategoryDto> categories = await categoryService.GetCategoriesByName(query);

        return TypedResults.Ok(categories);
    }

    private static async Task<CreatedAtRoute<CategoryDto>> CreateCategory(CreateCategoryDto categoryDto, CategoryService categoryService)
    {
        CategoryDto createdCategory = await categoryService.CreateCategory(categoryDto);

        return TypedResults.CreatedAtRoute(createdCategory, "GetCategoryById", new { idCategory = createdCategory.Id });
    }
}
