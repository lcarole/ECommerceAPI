using API_E_Commerce.DTO;
using API_E_Commerce.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_E_Commerce.Endpoints;

public static class ItemsEndpoints
{
    public static RouteGroupBuilder MapItemsEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllItems)
            .WithSummary("Get all items")
            .WithDescription("Retrieves a list of all items in the e-commerce platform.");

        group.MapGet("/{idItem:int}", GetItemById)
            .WithSummary("Get item by ID")
            .WithDescription("Retrieves a specific item by its unique identifier.");

        group.MapGet("/category/{categoryId:int}", GetItemByCategoryId)
            .WithSummary("Get items by category ID")
            .WithDescription("Retrieves a list of items that belong to a specific category.");

        group.MapGet("/available", GetAllAvailableItems)
            .WithSummary("Get all available items")
            .WithDescription("Retrieves a list of all items that are currently in stock.");

        group.MapGet("/search", GetItemsByName)
            .WithSummary("Search items by name")
            .WithDescription("Searches for items by their name and retrieves a list of matching items.");

        return group;
    }

    private static async Task<Ok<List<ItemDto>>> GetAllItems(ItemService itemService)
    {
        List<ItemDto> items = await itemService.GetAllItems();

        return TypedResults.Ok(items);
    }

    private static async Task<Results<Ok<ItemDto>, NotFound>> GetItemById(int idItem, ItemService itemService)
    {
        ItemDto? item = await itemService.GetItemById(idItem);

        return item == null ? TypedResults.NotFound() : TypedResults.Ok(item);
    }

    private static async Task<Ok<List<ItemDto>>> GetItemByCategoryId(int categoryId, ItemService itemService)
    {
        List<ItemDto> items = await itemService.GetItemByCategoryId(categoryId);

        return TypedResults.Ok(items);
    }

    private static async Task<Ok<List<ItemDto>>> GetAllAvailableItems(ItemService itemService)
    {
        List<ItemDto> items = await itemService.GetAllAvailableItems();

        return TypedResults.Ok(items);
    }

    private static async Task<Ok<List<ItemDto>>> GetItemsByName(string query, ItemService itemService)
    {
        List<ItemDto> items = await itemService.GetItemsByName(query);

        return TypedResults.Ok(items);
    }
}
