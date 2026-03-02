using API_E_Commerce.Models;

namespace API_E_Commerce.DTO;

public class OrderDto
{
    public int Id { get; set; }

    public decimal? Total { get; set; }

    public string? Status { get; set; }

    public OrderDto(Order order)
    {
        Id = order.Id;
        Total = order.Total;
        Status = order.Status;
    }
}