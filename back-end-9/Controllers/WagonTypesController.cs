using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.WagonTypes;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class WagonTypesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public WagonTypesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WagonTypeDTO>>> GetWagonTypes()
    {
        var wagonTypes = await _context.WagonTypes.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<WagonTypeDTO>>(wagonTypes));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WagonTypeDTO>> GetWagonType(Guid id)
    {
        var wagonType = await _context.WagonTypes.FindAsync(id);
        if (wagonType == null) return NotFound();
        return _mapper.Map<WagonTypeDTO>(wagonType);
    }

    [HttpPost]
    public async Task<ActionResult<WagonTypeDTO>> CreateWagonType(CreateWagonTypeDTO createDto)
    {
        var wagonType = _mapper.Map<WagonType>(createDto);
        wagonType.Id = Guid.NewGuid();
        
        _context.WagonTypes.Add(wagonType);
        await _context.SaveChangesAsync();

        var wagonTypeDto = _mapper.Map<WagonTypeDTO>(wagonType);
        return CreatedAtAction(nameof(GetWagonType), new { id = wagonTypeDto.Id }, wagonTypeDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWagonType(Guid id, UpdateWagonTypeDTO updateDto)
    {
        var wagonType = await _context.WagonTypes.FindAsync(id);
        if (wagonType == null) return NotFound();

        _mapper.Map(updateDto, wagonType);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWagonType(Guid id)
    {
        var wagonType = await _context.WagonTypes.FindAsync(id);
        if (wagonType == null) return NotFound();

        _context.WagonTypes.Remove(wagonType);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}