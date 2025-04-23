using back_end_9.Data;
using back_end_9.Models;
using back_end_9.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WheelPairsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WheelPairsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WheelPair>>> GetWheelPairs()
    {
        return await _context.WheelPairs.Include(w => w.Part).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WheelPair>> GetWheelPair(Guid id)
    {
        var wheelPair = await _context.WheelPairs
            .Include(w => w.Part)
            .FirstOrDefaultAsync(w => w.PartId == id);

        if (wheelPair == null) return NotFound();

        return wheelPair;
    }

    [HttpPost]
    public async Task<ActionResult<WheelPair>> CreateWheelPair(WheelPair wheelPair)
    {
        wheelPair.PartId = Guid.NewGuid();
        wheelPair.Part = new Part
        {
            PartId = wheelPair.PartId,
            PartType = PartType.wheel_pair,
            CreatedAt = DateTime.UtcNow
        };

        _context.WheelPairs.Add(wheelPair);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetWheelPair", new { id = wheelPair.PartId }, wheelPair);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWheelPair(Guid id, WheelPair wheelPair)
    {
        if (id != wheelPair.PartId) return BadRequest();

        _context.Entry(wheelPair).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WheelPairExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWheelPair(Guid id)
    {
        var wheelPair = await _context.WheelPairs.FindAsync(id);
        if (wheelPair == null) return NotFound();

        _context.WheelPairs.Remove(wheelPair);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WheelPairExists(Guid id) => _context.WheelPairs.Any(e => e.PartId == id);
}