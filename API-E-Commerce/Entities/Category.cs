namespace API_E_Commerce.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
