using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartInstallationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PartInstallationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartInstallation>>> GetPartInstallations()
    {
        return await _context.PartInstallations
            .Include(p => p.Part)
            .Include(p => p.Wagon)
            .Include(p => p.FromLocation)
            .Include(p => p.ToLocation)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartInstallation>> GetPartInstallation(Guid id)
    {
        var installation = await _context.PartInstallations
            .Include(p => p.Part)
            .Include(p => p.Wagon)
            .Include(p => p.FromLocation)
            .Include(p => p.ToLocation)
            .FirstOrDefaultAsync(p => p.InstallationId == id);

        if (installation == null) return NotFound();

        return installation;
    }

    [HttpPost]
    public async Task<ActionResult<PartInstallation>> CreatePartInstallation(PartInstallation installation)
    {
        installation.InstallationId = Guid.NewGuid();
        installation.InstalledAt = DateTime.UtcNow;

        _context.PartInstallations.Add(installation);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPartInstallation", new { id = installation.InstallationId }, installation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartInstallation(Guid id, PartInstallation installation)
    {
        if (id != installation.InstallationId) return BadRequest();

        _context.Entry(installation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PartInstallationExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePartInstallation(Guid id)
    {
        var installation = await _context.PartInstallations.FindAsync(id);
        if (installation == null) return NotFound();

        _context.PartInstallations.Remove(installation);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PartInstallationExists(Guid id) => _context.PartInstallations.Any(e => e.InstallationId == id);
}