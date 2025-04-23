using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrarsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RegistrarsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Registrars
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Registrar>>> GetRegistrars()
    {
        return await _context.Registrars.ToListAsync();
    }

    // GET: api/Registrars/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Registrar>> GetRegistrar(Guid id)
    {
        var registrar = await _context.Registrars.FindAsync(id);

        if (registrar == null)
        {
            return NotFound();
        }

        return registrar;
    }

    // POST: api/Registrars
    [HttpPost]
    public async Task<ActionResult<Registrar>> PostRegistrar(Registrar registrar)
    {
        registrar.Id = Guid.NewGuid();
        _context.Registrars.Add(registrar);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRegistrar", new { id = registrar.Id }, registrar);
    }

    // PUT: api/Registrars/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistrar(Guid id, Registrar registrar)
    {
        if (id != registrar.Id)
        {
            return BadRequest();
        }

        _context.Entry(registrar).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RegistrarExists(id))
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

    // DELETE: api/Registrars/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistrar(Guid id)
    {
        var registrar = await _context.Registrars.FindAsync(id);
        if (registrar == null)
        {
            return NotFound();
        }

        _context.Registrars.Remove(registrar);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RegistrarExists(Guid id)
    {
        return _context.Registrars.Any(e => e.Id == id);
    }
}