using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.PartInstallations;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class PartInstallationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PartInstallationsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartInstallationDTO>>> GetPartInstallations()
    {
        var installations = await _context.PartInstallations
            .Include(p => p.Part)
            .Include(p => p.Wagon)
            .Include(p => p.FromLocation)
            .Include(p => p.ToLocation)
            .ToListAsync();
        return Ok(_mapper.Map<IEnumerable<PartInstallationDTO>>(installations));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartInstallationDTO>> GetPartInstallation(Guid id)
    {
        var installation = await _context.PartInstallations
            .Include(p => p.Part)
            .Include(p => p.Wagon)
            .Include(p => p.FromLocation)
            .Include(p => p.ToLocation)
            .FirstOrDefaultAsync(p => p.InstallationId == id);
        if (installation == null) return NotFound();
        return _mapper.Map<PartInstallationDTO>(installation);
    }

    [HttpPost]
    public async Task<ActionResult<PartInstallationDTO>> CreatePartInstallation(CreatePartInstallationDTO createDto)
    {
        var installation = _mapper.Map<PartInstallation>(createDto);
        installation.InstallationId = Guid.NewGuid();
        installation.InstalledAt = DateTime.UtcNow;
        
        _context.PartInstallations.Add(installation);
        await _context.SaveChangesAsync();

        var installationDto = _mapper.Map<PartInstallationDTO>(installation);
        return CreatedAtAction(nameof(GetPartInstallation), new { id = installationDto.InstallationId }, installationDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePartInstallation(Guid id, UpdatePartInstallationDTO updateDto)
    {
        var installation = await _context.PartInstallations.FindAsync(id);
        if (installation == null) return NotFound();

        _mapper.Map(updateDto, installation);
        await _context.SaveChangesAsync();

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
}