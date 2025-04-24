using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.Bolsters;
using back_end_9.Models;
using back_end_9.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class BolstersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public BolstersController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BolsterDTO>>> GetBolsters()
    {
        var bolsters = await _context.Bolsters
            .Include(b => b.Part)
            .ToListAsync();
        return Ok(_mapper.Map<IEnumerable<BolsterDTO>>(bolsters));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BolsterDTO>> GetBolster(Guid id)
    {
        var bolster = await _context.Bolsters
            .Include(b => b.Part)
            .FirstOrDefaultAsync(b => b.PartId == id);
        if (bolster == null) return NotFound();
        return _mapper.Map<BolsterDTO>(bolster);
    }

    [HttpPost]
    public async Task<ActionResult<BolsterDTO>> CreateBolster(CreateBolsterDTO createDto)
    {
        var bolster = _mapper.Map<Bolster>(createDto);
        bolster.PartId = Guid.NewGuid();
        bolster.Part = _mapper.Map<Part>(createDto.Part);
        bolster.Part.PartId = bolster.PartId;
        bolster.Part.PartType = PartType.bolster;
        bolster.Part.CreatedAt = DateTime.UtcNow;
        
        _context.Bolsters.Add(bolster);
        await _context.SaveChangesAsync();

        var bolsterDto = _mapper.Map<BolsterDTO>(bolster);
        return CreatedAtAction(nameof(GetBolster), new { id = bolsterDto.PartId }, bolsterDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBolster(Guid id, UpdateBolsterDTO updateDto)
    {
        var bolster = await _context.Bolsters
            .Include(b => b.Part)
            .FirstOrDefaultAsync(b => b.PartId == id);
        if (bolster == null) return NotFound();

        _mapper.Map(updateDto, bolster);
        _mapper.Map(updateDto.Part, bolster.Part);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBolster(Guid id)
    {
        var bolster = await _context.Bolsters.FindAsync(id);
        if (bolster == null) return NotFound();

        _context.Bolsters.Remove(bolster);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}