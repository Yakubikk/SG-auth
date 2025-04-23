using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WagonTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public WagonTypesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/WagonTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WagonType>>> GetWagonTypes()
    {
        return await _context.WagonTypes.ToListAsync();
    }

    // GET: api/WagonTypes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<WagonType>> GetWagonType(Guid id)
    {
        var wagonType = await _context.WagonTypes.FindAsync(id);

        if (wagonType == null)
        {
            return NotFound();
        }

        return wagonType;
    }

    // POST: api/WagonTypes
    [HttpPost]
    public async Task<ActionResult<WagonType>> PostWagonType(WagonType wagonType)
    {
        wagonType.Id = Guid.NewGuid();
        _context.WagonTypes.Add(wagonType);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetWagonType", new { id = wagonType.Id }, wagonType);
    }

    // PUT: api/WagonTypes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWagonType(Guid id, WagonType wagonType)
    {
        if (id != wagonType.Id)
        {
            return BadRequest();
        }

        _context.Entry(wagonType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WagonTypeExists(id))
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

    // DELETE: api/WagonTypes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWagonType(Guid id)
    {
        var wagonType = await _context.WagonTypes.FindAsync(id);
        if (wagonType == null)
        {
            return NotFound();
        }

        _context.WagonTypes.Remove(wagonType);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WagonTypeExists(Guid id)
    {
        return _context.WagonTypes.Any(e => e.Id == id);
    }
}