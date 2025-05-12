using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.RailwayCisterns;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class RailwayCisternsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RailwayCisternsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RailwayCisternDTO>>> GetRailwayCisterns()
    {
        var cisterns = await _context.RailwayCisterns
            .Include(rc => rc.Manufacturer)
            .Include(rc => rc.WagonType)
            .Include(rc => rc.Registrar)
            .Include(rc => rc.WagonModel) // Добавляем включение модели
            .ToListAsync();
        return Ok(_mapper.Map<IEnumerable<RailwayCisternDTO>>(cisterns));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RailwayCisternDTO>> GetRailwayCistern(Guid id)
    {
        var cistern = await _context.RailwayCisterns
            .Include(rc => rc.Manufacturer)
            .Include(rc => rc.WagonType)
            .Include(rc => rc.Registrar)
            .Include(rc => rc.WagonModel) // Добавляем включение модели
            .FirstOrDefaultAsync(rc => rc.Id == id);
        if (cistern == null) return NotFound();
        return _mapper.Map<RailwayCisternDTO>(cistern);
    }

    [HttpPost]
    public async Task<ActionResult<RailwayCisternDTO>> CreateRailwayCistern(CreateRailwayCisternDTO createDto)
    {
        var cistern = _mapper.Map<RailwayCistern>(createDto);
        cistern.Id = Guid.NewGuid();
        cistern.CreatedAt = DateTime.UtcNow;
        cistern.UpdatedAt = DateTime.UtcNow;
        
        _context.RailwayCisterns.Add(cistern);
        await _context.SaveChangesAsync();

        var cisternDto = _mapper.Map<RailwayCisternDTO>(cistern);
        return CreatedAtAction(nameof(GetRailwayCistern), new { id = cisternDto.Id }, cisternDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRailwayCistern(Guid id, UpdateRailwayCisternDTO updateDto)
    {
        var cistern = await _context.RailwayCisterns.FindAsync(id);
        if (cistern == null) return NotFound();

        _mapper.Map(updateDto, cistern);
        cistern.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRailwayCistern(Guid id)
    {
        var cistern = await _context.RailwayCisterns.FindAsync(id);
        if (cistern == null) return NotFound();

        _context.RailwayCisterns.Remove(cistern);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}