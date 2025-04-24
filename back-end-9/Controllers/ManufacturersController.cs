using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.Manufacturers;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class ManufacturersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ManufacturersController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ManufacturerDTO>>> GetManufacturers()
    {
        var manufacturers = await _context.Manufacturers.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ManufacturerDTO>>(manufacturers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManufacturerDTO>> GetManufacturer(Guid id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer == null) return NotFound();
        return _mapper.Map<ManufacturerDTO>(manufacturer);
    }

    [HttpPost]
    public async Task<ActionResult<ManufacturerDTO>> CreateManufacturer(CreateManufacturerDTO createDto)
    {
        var manufacturer = _mapper.Map<Manufacturer>(createDto);
        manufacturer.Id = Guid.NewGuid();
        
        _context.Manufacturers.Add(manufacturer);
        await _context.SaveChangesAsync();

        var manufacturerDto = _mapper.Map<ManufacturerDTO>(manufacturer);
        return CreatedAtAction(nameof(GetManufacturer), new { id = manufacturerDto.Id }, manufacturerDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateManufacturer(Guid id, UpdateManufacturerDTO updateDto)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer == null) return NotFound();

        _mapper.Map(updateDto, manufacturer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManufacturer(Guid id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer == null) return NotFound();

        _context.Manufacturers.Remove(manufacturer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}