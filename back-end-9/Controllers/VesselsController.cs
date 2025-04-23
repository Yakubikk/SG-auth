using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VesselsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VesselsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Vessels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vessel>>> GetVessels()
    {
        return await _context.Vessels
            .Include(v => v.RailwayCistern)
            .ToListAsync();
    }

    // GET: api/Vessels/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Vessel>> GetVessel(Guid id)
    {
        var vessel = await _context.Vessels
            .Include(v => v.RailwayCistern)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vessel == null)
        {
            return NotFound();
        }

        return vessel;
    }

    // POST: api/Vessels
    [HttpPost]
    public async Task<ActionResult<Vessel>> PostVessel(Vessel vessel)
    {
        vessel.Id = Guid.NewGuid();
        _context.Vessels.Add(vessel);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetVessel", new { id = vessel.Id }, vessel);
    }

    // PUT: api/Vessels/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVessel(Guid id, Vessel vessel)
    {
        if (id != vessel.Id)
        {
            return BadRequest();
        }

        _context.Entry(vessel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VesselExists(id))
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

    // DELETE: api/Vessels/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVessel(Guid id)
    {
        var vessel = await _context.Vessels.FindAsync(id);
        if (vessel == null)
        {
            return NotFound();
        }

        _context.Vessels.Remove(vessel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VesselExists(Guid id)
    {
        return _context.Vessels.Any(e => e.Id == id);
    }
}