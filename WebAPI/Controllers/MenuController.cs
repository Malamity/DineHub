using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems()
    {
        var menuItems = await _menuService.GetMenuItemsAsync();
        return Ok(menuItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuItem(int id)
    {
        var menuItem = await _menuService.GetMenuItemAsync(id);
        if (menuItem == null) return NotFound();
        return Ok(menuItem);
    }

    /// <summary>
    /// Creates a new menu item
    /// </summary>
    /// <param name="menuItem"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItem menuItem)
    {
        await _menuService.CreateMenuItemAsync(menuItem);
        return CreatedAtAction(nameof(GetMenuItem), new { id = menuItem.Id }, menuItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItem menuItem)
    {
        if (id != menuItem.Id) return BadRequest("ID mismatch");

        var existingItem = await _menuService.GetMenuItemAsync(id);
        if (existingItem == null) return NotFound();

        await _menuService.UpdateMenuItemAsync(menuItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(int id)
    {
        var menuItem = await _menuService.GetMenuItemAsync(id);
        if (menuItem == null) return NotFound();

        await _menuService.DeleteMenuItemAsync(id);
        return NoContent();
    }
}
