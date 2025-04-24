using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.Registrars;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrarsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RegistrarsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistrarDTO>>> GetRegistrars()
    {
        var registrars = await _context.Registrars.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<RegistrarDTO>>(registrars));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RegistrarDTO>> GetRegistrar(Guid id)
    {
        var registrar = await _context.Registrars.FindAsync(id);
        if (registrar == null) return NotFound();
        return _mapper.Map<RegistrarDTO>(registrar);
    }

    [HttpPost]
    public async Task<ActionResult<RegistrarDTO>> CreateRegistrar(CreateRegistrarDTO createDto)
    {
        var registrar = _mapper.Map<Registrar>(createDto);
        registrar.Id = Guid.NewGuid();
        
        _context.Registrars.Add(registrar);
        await _context.SaveChangesAsync();

        var registrarDto = _mapper.Map<RegistrarDTO>(registrar);
        return CreatedAtAction(nameof(GetRegistrar), new { id = registrarDto.Id }, registrarDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegistrar(Guid id, UpdateRegistrarDTO updateDto)
    {
        var registrar = await _context.Registrars.FindAsync(id);
        if (registrar == null) return NotFound();

        _mapper.Map(updateDto, registrar);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistrar(Guid id)
    {
        var registrar = await _context.Registrars.FindAsync(id);
        if (registrar == null) return NotFound();

        _context.Registrars.Remove(registrar);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}