using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.ShockAbsorbers;
using back_end_9.Models;
using back_end_9.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class ShockAbsorbersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ShockAbsorbersController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShockAbsorberDTO>>> GetShockAbsorbers()
    {
        var shockAbsorbers = await _context.ShockAbsorbers
            .Include(s => s.Part)
            .ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ShockAbsorberDTO>>(shockAbsorbers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShockAbsorberDTO>> GetShockAbsorber(Guid id)
    {
        var shockAbsorber = await _context.ShockAbsorbers
            .Include(s => s.Part)
            .FirstOrDefaultAsync(s => s.PartId == id);
        if (shockAbsorber == null) return NotFound();
        return _mapper.Map<ShockAbsorberDTO>(shockAbsorber);
    }

    [HttpPost]
    public async Task<ActionResult<ShockAbsorberDTO>> CreateShockAbsorber(CreateShockAbsorberDTO createDto)
    {
        var shockAbsorber = _mapper.Map<ShockAbsorber>(createDto);
        shockAbsorber.PartId = Guid.NewGuid();
        shockAbsorber.Part = _mapper.Map<Part>(createDto.Part);
        shockAbsorber.Part.PartId = shockAbsorber.PartId;
        shockAbsorber.Part.PartType = PartType.shock_absorber;
        shockAbsorber.Part.CreatedAt = DateTime.UtcNow;
        
        _context.ShockAbsorbers.Add(shockAbsorber);
        await _context.SaveChangesAsync();

        var shockAbsorberDto = _mapper.Map<ShockAbsorberDTO>(shockAbsorber);
        return CreatedAtAction(nameof(GetShockAbsorber), new { id = shockAbsorberDto.PartId }, shockAbsorberDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShockAbsorber(Guid id, UpdateShockAbsorberDTO updateDto)
    {
        var shockAbsorber = await _context.ShockAbsorbers
            .Include(s => s.Part)
            .FirstOrDefaultAsync(s => s.PartId == id);
        if (shockAbsorber == null) return NotFound();

        _mapper.Map(updateDto, shockAbsorber);
        _mapper.Map(updateDto.Part, shockAbsorber.Part);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShockAbsorber(Guid id)
    {
        var shockAbsorber = await _context.ShockAbsorbers.FindAsync(id);
        if (shockAbsorber == null) return NotFound();

        _context.ShockAbsorbers.Remove(shockAbsorber);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}