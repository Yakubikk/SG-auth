using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.SideFrames;
using back_end_9.Models;
using back_end_9.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class SideFramesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SideFramesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SideFrameDTO>>> GetSideFrames()
    {
        var sideFrames = await _context.SideFrames
            .Include(s => s.Part)
            .ToListAsync();
        return Ok(_mapper.Map<IEnumerable<SideFrameDTO>>(sideFrames));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SideFrameDTO>> GetSideFrame(Guid id)
    {
        var sideFrame = await _context.SideFrames
            .Include(s => s.Part)
            .FirstOrDefaultAsync(s => s.PartId == id);
        if (sideFrame == null) return NotFound();
        return _mapper.Map<SideFrameDTO>(sideFrame);
    }

    [HttpPost]
    public async Task<ActionResult<SideFrameDTO>> CreateSideFrame(CreateSideFrameDTO createDto)
    {
        var sideFrame = _mapper.Map<SideFrame>(createDto);
        sideFrame.PartId = Guid.NewGuid();
        sideFrame.Part = _mapper.Map<Part>(createDto.Part);
        sideFrame.Part.PartId = sideFrame.PartId;
        sideFrame.Part.PartType = PartType.side_frame;
        sideFrame.Part.CreatedAt = DateTime.UtcNow;
        
        _context.SideFrames.Add(sideFrame);
        await _context.SaveChangesAsync();

        var sideFrameDto = _mapper.Map<SideFrameDTO>(sideFrame);
        return CreatedAtAction(nameof(GetSideFrame), new { id = sideFrameDto.PartId }, sideFrameDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSideFrame(Guid id, UpdateSideFrameDTO updateDto)
    {
        var sideFrame = await _context.SideFrames
            .Include(s => s.Part)
            .FirstOrDefaultAsync(s => s.PartId == id);
        if (sideFrame == null) return NotFound();

        _mapper.Map(updateDto, sideFrame);
        _mapper.Map(updateDto.Part, sideFrame.Part);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSideFrame(Guid id)
    {
        var sideFrame = await _context.SideFrames.FindAsync(id);
        if (sideFrame == null) return NotFound();

        _context.SideFrames.Remove(sideFrame);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
