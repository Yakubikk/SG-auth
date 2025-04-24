using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.WheelPairs;
using back_end_9.Models;
using back_end_9.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class WheelPairsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public WheelPairsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WheelPairDTO>>> GetWheelPairs()
    {
        var wheelPairs = await _context.WheelPairs
            .Include(w => w.Part)
            .ToListAsync();
        return Ok(_mapper.Map<IEnumerable<WheelPairDTO>>(wheelPairs));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WheelPairDTO>> GetWheelPair(Guid id)
    {
        var wheelPair = await _context.WheelPairs
            .Include(w => w.Part)
            .FirstOrDefaultAsync(w => w.PartId == id);
        if (wheelPair == null) return NotFound();
        return _mapper.Map<WheelPairDTO>(wheelPair);
    }

    [HttpPost]
    public async Task<ActionResult<WheelPairDTO>> CreateWheelPair(CreateWheelPairDTO createDto)
    {
        var wheelPair = _mapper.Map<WheelPair>(createDto);
        wheelPair.PartId = Guid.NewGuid();
        wheelPair.Part = _mapper.Map<Part>(createDto.Part);
        wheelPair.Part.PartId = wheelPair.PartId;
        wheelPair.Part.PartType = PartType.wheel_pair;
        wheelPair.Part.CreatedAt = DateTime.UtcNow;
        
        _context.WheelPairs.Add(wheelPair);
        await _context.SaveChangesAsync();

        var wheelPairDto = _mapper.Map<WheelPairDTO>(wheelPair);
        return CreatedAtAction(nameof(GetWheelPair), new { id = wheelPairDto.PartId }, wheelPairDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWheelPair(Guid id, UpdateWheelPairDTO updateDto)
    {
        var wheelPair = await _context.WheelPairs
            .Include(w => w.Part)
            .FirstOrDefaultAsync(w => w.PartId == id);
        if (wheelPair == null) return NotFound();

        _mapper.Map(updateDto, wheelPair);
        _mapper.Map(updateDto.Part, wheelPair.Part);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWheelPair(Guid id)
    {
        var wheelPair = await _context.WheelPairs.FindAsync(id);
        if (wheelPair == null) return NotFound();

        _context.WheelPairs.Remove(wheelPair);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}