using back_end_9.Data;
using back_end_9.Models;
using back_end_9.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouplerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CouplerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coupler>>> GetCouplers()
    {
        return await _context.Couplers.Include(w => w.Part).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Coupler>> GetCoupler(Guid id)
    {
        var coupler = await _context.Couplers
            .Include(w => w.Part)
            .FirstOrDefaultAsync(w => w.PartId == id);

        if (coupler == null) return NotFound();

        return coupler;
    }

    [HttpPost]
    public async Task<ActionResult<Coupler>> CreateCoupler(Coupler coupler)
    {
        coupler.PartId = Guid.NewGuid();
        coupler.Part = new Part
        {
            PartId = coupler.PartId,
            PartType = PartType.coupler,
            CreatedAt = DateTime.UtcNow
        };

        _context.Couplers.Add(coupler);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCoupler", new { id = coupler.PartId }, coupler);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCoupler(Guid id, Coupler coupler)
    {
        if (id != coupler.PartId) return BadRequest();

        _context.Entry(coupler).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CouplerExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupler(Guid id)
    {
        var coupler = await _context.Couplers.FindAsync(id);
        if (coupler == null) return NotFound();

        _context.Couplers.Remove(coupler);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CouplerExists(Guid id) => _context.Couplers.Any(e => e.PartId == id);
}