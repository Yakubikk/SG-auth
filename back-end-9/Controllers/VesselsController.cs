using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.Vessels;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class VesselsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public VesselsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VesselDTO>>> GetVessels()
    {
        var vessels = await _context.Vessels
            .Include(v => v.RailwayCistern)
            .ToListAsync();
        return Ok(_mapper.Map<IEnumerable<VesselDTO>>(vessels));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VesselDTO>> GetVessel(Guid id)
    {
        var vessel = await _context.Vessels
            .Include(v => v.RailwayCistern)
            .FirstOrDefaultAsync(v => v.Id == id);
        if (vessel == null) return NotFound();
        return _mapper.Map<VesselDTO>(vessel);
    }

    [HttpPost]
    public async Task<ActionResult<VesselDTO>> CreateVessel(CreateVesselDTO createDto)
    {
        var vessel = _mapper.Map<Vessel>(createDto);
        vessel.Id = Guid.NewGuid();
        
        _context.Vessels.Add(vessel);
        await _context.SaveChangesAsync();

        var vesselDto = _mapper.Map<VesselDTO>(vessel);
        return CreatedAtAction(nameof(GetVessel), new { id = vesselDto.Id }, vesselDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVessel(Guid id, UpdateVesselDTO updateDto)
    {
        var vessel = await _context.Vessels.FindAsync(id);
        if (vessel == null) return NotFound();

        _mapper.Map(updateDto, vessel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVessel(Guid id)
    {
        var vessel = await _context.Vessels.FindAsync(id);
        if (vessel == null) return NotFound();

        _context.Vessels.Remove(vessel);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}