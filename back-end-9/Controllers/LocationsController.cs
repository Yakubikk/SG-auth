using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LocationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
    {
        return await _context.Locations.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Location>> GetLocation(Guid id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();

        return location;
    }

    [HttpPost]
    public async Task<ActionResult<Location>> CreateLocation(Location location)
    {
        location.LocationId = Guid.NewGuid();
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetLocation", new { id = location.LocationId }, location);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(Guid id, Location location)
    {
        if (id != location.LocationId) return BadRequest();

        _context.Entry(location).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LocationExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(Guid id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LocationExists(Guid id) => _context.Locations.Any(e => e.LocationId == id);
}