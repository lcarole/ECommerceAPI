using API_E_Commerce.Entities;

namespace API_E_Commerce.DTO;

public class ItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public int IdCategory { get; set; }

    public int Stock { get; set; }

    public string? ImageUrl { get; set; }

    public ItemDto(Item item)
    {
        Id = item.Id;
        Name = item.Name;
        Price = item.Price;
        Description = item.Description;
        IdCategory = item.IdCategory;
        Stock = item.Stock;
        ImageUrl = item.ImageUrl;
    }
}