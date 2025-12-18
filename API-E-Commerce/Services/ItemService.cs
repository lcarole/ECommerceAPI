using API_E_Commerce.Contexts;
using API_E_Commerce.DTO;
using API_E_Commerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace API_E_Commerce.Services;

public class ItemService
{
    private readonly ECommerceContext _context;
    private readonly IMemoryCache _cache;
    
    public ItemService(ECommerceContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }
    
    public async Task<List<ItemDto>> GetAllItems()
    {
        List<ItemDto> items = new();

        if (!_cache.TryGetValue("AllItems", out items))
        {
            items = await _context
                .Items
                .OrderBy(i => i.Name)
                .Select(i => new ItemDto(i))
                .ToListAsync();

            _cache.Set("AllItems", items, TimeSpan.FromMinutes(1));
        }

        return items;
    }

    public async Task<ItemDto?> GetItemById(int id)
    {
        Item? item = await _context.Items.FindAsync(id);
        ItemDto? itemDto = item != null ? new ItemDto(item) : null;
        
        return itemDto;
        
    }

    public async Task<List<ItemDto>> GetItemByCategoryId(int categoryId)
    {
        List<ItemDto> items = await _context
            .Items
            .Where(i => i.IdCategory == categoryId)
            .OrderBy(i => i.Name)
            .Select(i => new ItemDto(i))
            .ToListAsync();
        
        return items;
    }

    public async Task<List<ItemDto>> GetAllAvailableItems()
    {
        List<ItemDto> items = await _context
            .Items
            .Where(i => i.Stock > 0)
            .OrderBy(i => i.Name)
            .Select(i => new ItemDto(i))
            .ToListAsync();
        
        return items;
    }
    
    public async Task<List<ItemDto>> GetItemsByName(string name)
    {
        List<ItemDto> items = await _context
            .Items
            .Where(i => EF.Functions.Like(i.Name.ToLower(),$"%{name.ToLower()}%" ))
            .OrderBy(i => i.Name)
            .Select(i => new ItemDto(i))
            .ToListAsync();
        
        return items;
    }
}