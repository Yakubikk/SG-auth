using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RailwayCisternsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RailwayCisternsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/RailwayCisterns
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RailwayCistern>>> GetRailwayCisterns()
    {
        return await _context.RailwayCisterns
            .Include(rc => rc.Manufacturer)
            .Include(rc => rc.WagonType)
            .Include(rc => rc.Registrar)
            .ToListAsync();
    }

    // GET: api/RailwayCisterns/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RailwayCistern>> GetRailwayCistern(Guid id)
    {
        var railwayCistern = await _context.RailwayCisterns
            .Include(rc => rc.Manufacturer)
            .Include(rc => rc.WagonType)
            .Include(rc => rc.Registrar)
            .FirstOrDefaultAsync(rc => rc.Id == id);

        if (railwayCistern == null)
        {
            return NotFound();
        }

        return railwayCistern;
    }

    // POST: api/RailwayCisterns
    [HttpPost]
    public async Task<ActionResult<RailwayCistern>> PostRailwayCistern(RailwayCistern railwayCistern)
    {
        railwayCistern.Id = Guid.NewGuid();
        railwayCistern.CreatedAt = DateTime.UtcNow;
        railwayCistern.UpdatedAt = DateTime.UtcNow;

        _context.RailwayCisterns.Add(railwayCistern);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRailwayCistern", new { id = railwayCistern.Id }, railwayCistern);
    }

    // PUT: api/RailwayCisterns/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRailwayCistern(Guid id, RailwayCistern railwayCistern)
    {
        if (id != railwayCistern.Id)
        {
            return BadRequest();
        }

        railwayCistern.UpdatedAt = DateTime.UtcNow;
        _context.Entry(railwayCistern).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RailwayCisternExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/RailwayCisterns/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRailwayCistern(Guid id)
    {
        var railwayCistern = await _context.RailwayCisterns.FindAsync(id);
        if (railwayCistern == null)
        {
            return NotFound();
        }

        _context.RailwayCisterns.Remove(railwayCistern);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RailwayCisternExists(Guid id)
    {
        return _context.RailwayCisterns.Any(e => e.Id == id);
    }
}