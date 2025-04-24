using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.Parts;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class PartsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PartsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartDTO>>> GetParts()
    {
        var parts = await _context.Parts.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<PartDTO>>(parts));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PartDTO>> GetPart(Guid id)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null) return NotFound();
        return _mapper.Map<PartDTO>(part);
    }

    [HttpPost]
    public async Task<ActionResult<PartDTO>> CreatePart(CreatePartDTO createDto)
    {
        var part = _mapper.Map<Part>(createDto);
        part.PartId = Guid.NewGuid();
        part.CreatedAt = DateTime.UtcNow;
        
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();

        var partDto = _mapper.Map<PartDTO>(part);
        return CreatedAtAction(nameof(GetPart), new { id = partDto.PartId }, partDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePart(Guid id, UpdatePartDTO updateDto)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null) return NotFound();

        _mapper.Map(updateDto, part);
        await _context.SaveChangesAsync();

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
}