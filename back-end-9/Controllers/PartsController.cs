using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PartsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Part>>> GetParts()
    {
        return await _context.Parts.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Part>> GetPart(Guid id)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null) return NotFound();

        return part;
    }

    [HttpPost]
    public async Task<ActionResult<Part>> CreatePart(Part part)
    {
        part.PartId = Guid.NewGuid();
        part.CreatedAt = DateTime.UtcNow;
        
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPart", new { id = part.PartId }, part);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePart(Guid id, Part part)
    {
        if (id != part.PartId) return BadRequest();

        _context.Entry(part).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PartExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePart(Guid id)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null) return NotFound();

        _context.Parts.Remove(part);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PartExists(Guid id) => _context.Parts.Any(e => e.PartId == id);
}