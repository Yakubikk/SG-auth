using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.Locations;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LocationsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
    {
        var locations = await _context.Locations.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<LocationDTO>>(locations));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDTO>> GetLocation(Guid id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();
        return _mapper.Map<LocationDTO>(location);
    }

    [HttpPost]
    public async Task<ActionResult<LocationDTO>> CreateLocation(CreateLocationDTO createDto)
    {
        var location = _mapper.Map<Location>(createDto);
        location.LocationId = Guid.NewGuid();
        
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();

        var locationDto = _mapper.Map<LocationDTO>(location);
        return CreatedAtAction(nameof(GetLocation), new { id = locationDto.LocationId }, locationDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLocation(Guid id, UpdateLocationDTO updateDto)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();

        _mapper.Map(updateDto, location);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLocation(Guid id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}