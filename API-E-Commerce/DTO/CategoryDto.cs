using API_E_Commerce.Models;

namespace API_E_Commerce.DTO;

public class CategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public CategoryDto(Category category)
    {
        Id = category.Id;
        Name = category.Name;
        Description = category.Description;
    }
}