namespace API_E_Commerce.Models;

public partial class Order
{
    public int Id { get; set; }

    public decimal? Total { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
}
