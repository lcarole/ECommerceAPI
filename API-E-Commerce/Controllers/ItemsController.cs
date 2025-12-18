using API_E_Commerce.DTO;
using API_E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_E_Commerce.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ItemsController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemsController(ItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    [EndpointSummary("Get all items")]
    [EndpointDescription("Retrieves a list of all items in the e-commerce platform.")]
    public async Task<ActionResult<List<ItemDto>>> GetAllItems()
    {
        List<ItemDto> items = await _itemService.GetAllItems();
            
        return Ok(items);
    }

    [HttpGet("{idItem}")]
    [EndpointSummary("Get item by ID")]
    [EndpointDescription("Retrieves a specific item by its unique identifier.")]
    public async Task<ActionResult<ItemDto>> GetItemById(int idItem)
    {
        ItemDto? item = await _itemService.GetItemById(idItem);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet("category/{categoryId}")]
    [EndpointSummary("Get items by category ID")]
    [EndpointDescription("Retrieves a list of items that belong to a specific category.")]
    public async Task<ActionResult<List<ItemDto>>> GetItemByCategoryId(int categoryId)
    {
        List<ItemDto> items = await _itemService.GetItemByCategoryId(categoryId);
        return Ok(items);
    }

    [HttpGet("available")]
    [EndpointSummary("Get all available items")]
    [EndpointDescription("Retrieves a list of all items that are currently in stock.")]
    public async Task<ActionResult<List<ItemDto>>> GetAllAvailableItems()
    {
        List<ItemDto> items = await _itemService.GetAllAvailableItems();
        return Ok(items);
    }

    [HttpGet("search/{query}")]
    [EndpointSummary("Search items by name")]
    [EndpointDescription("Searches for items by their name and retrieves a list of matching items.")]
    public async Task<ActionResult<List<ItemDto>>> GetItemsByName(string query)
    {
        List<ItemDto> items = await _itemService.GetItemsByName(query);
        return Ok(items);
    }
}
