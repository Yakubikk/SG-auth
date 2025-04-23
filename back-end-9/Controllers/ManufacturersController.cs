using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ManufacturersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Manufacturers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
    {
        return await _context.Manufacturers.ToListAsync();
    }

    // GET: api/Manufacturers/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Manufacturer>> GetManufacturer(Guid id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);

        if (manufacturer == null)
        {
            return NotFound();
        }

        return manufacturer;
    }

    // POST: api/Manufacturers
    [HttpPost]
    public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer manufacturer)
    {
        manufacturer.Id = Guid.NewGuid();
        _context.Manufacturers.Add(manufacturer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetManufacturer", new { id = manufacturer.Id }, manufacturer);
    }

    // PUT: api/Manufacturers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutManufacturer(Guid id, Manufacturer manufacturer)
    {
        if (id != manufacturer.Id)
        {
            return BadRequest();
        }

        _context.Entry(manufacturer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ManufacturerExists(id))
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

    // DELETE: api/Manufacturers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManufacturer(Guid id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }

        _context.Manufacturers.Remove(manufacturer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ManufacturerExists(Guid id)
    {
        return _context.Manufacturers.Any(e => e.Id == id);
    }
}