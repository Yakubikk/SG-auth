using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.WagonModel;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WagonModelsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public WagonModelsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WagonModelDTO>>> GetWagonModels()
    {
        var models = await _context.WagonModels.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<WagonModelDTO>>(models));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WagonModelDTO>> GetWagonModel(Guid id)
    {
        var model = await _context.WagonModels.FindAsync(id);
        if (model == null) return NotFound();
        return _mapper.Map<WagonModelDTO>(model);
    }

    [HttpPost]
    public async Task<ActionResult<WagonModelDTO>> CreateWagonModel(CreateWagonModelDTO createDto)
    {
        if (await _context.WagonModels.AnyAsync(m => m.Name == createDto.Name))
            return Conflict("Model with this name already exists");

        var model = _mapper.Map<WagonModel>(createDto);
        model.ModelId = Guid.NewGuid();
        
        _context.WagonModels.Add(model);
        await _context.SaveChangesAsync();

        var modelDto = _mapper.Map<WagonModelDTO>(model);
        return CreatedAtAction(nameof(GetWagonModel), new { id = modelDto.ModelId }, modelDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWagonModel(Guid id, UpdateWagonModelDTO updateDto)
    {
        var model = await _context.WagonModels.FindAsync(id);
        if (model == null) return NotFound();

        if (await _context.WagonModels.AnyAsync(m => m.Name == updateDto.Name && m.ModelId != id))
            return Conflict("Another model with this name already exists");

        _mapper.Map(updateDto, model);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWagonModel(Guid id)
    {
        var model = await _context.WagonModels.FindAsync(id);
        if (model == null) return NotFound();

        if (await _context.RailwayCisterns.AnyAsync(rc => rc.ModelId == id))
            return BadRequest("Cannot delete model - it is referenced by railway cisterns");

        _context.WagonModels.Remove(model);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}