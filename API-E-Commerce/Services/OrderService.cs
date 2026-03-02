using API_E_Commerce.Contexts;
using API_E_Commerce.DTO;
using Microsoft.EntityFrameworkCore;

namespace API_E_Commerce.Services;

public class OrderService
{
    private readonly ECommerceContext _context;

    public OrderService(ECommerceContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        List<OrderDto> orders = await _context.Orders
            .Select(o => new OrderDto(o))
            .ToListAsync();

        return orders;
    }
}