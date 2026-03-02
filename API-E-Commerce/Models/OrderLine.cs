namespace API_E_Commerce.Models;

public partial class OrderLine
{
    public int IdOrder { get; set; }

    public int IdItem { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Item IdItemNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
