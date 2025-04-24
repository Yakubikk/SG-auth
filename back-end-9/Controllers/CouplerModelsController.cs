using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.CouplerModels;
using back_end_9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class CouplerModelsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CouplerModelsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CouplerModelDTO>>> GetCouplerModels()
    {
        var models = await _context.CouplerModels.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<CouplerModelDTO>>(models));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CouplerModelDTO>> GetCouplerModel(Guid id)
    {
        var model = await _context.CouplerModels.FindAsync(id);
        if (model == null) return NotFound();
        return _mapper.Map<CouplerModelDTO>(model);
    }

    [HttpPost]
    public async Task<ActionResult<CouplerModelDTO>> CreateCouplerModel(CreateCouplerModelDTO createDto)
    {
        var model = _mapper.Map<CouplerModel>(createDto);
        model.Id = Guid.NewGuid();
        
        _context.CouplerModels.Add(model);
        await _context.SaveChangesAsync();

        var modelDto = _mapper.Map<CouplerModelDTO>(model);
        return CreatedAtAction(nameof(GetCouplerModel), new { id = modelDto.Id }, modelDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCouplerModel(Guid id, UpdateCouplerModelDTO updateDto)
    {
        var model = await _context.CouplerModels.FindAsync(id);
        if (model == null) return NotFound();

        _mapper.Map(updateDto, model);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCouplerModel(Guid id)
    {
        var model = await _context.CouplerModels.FindAsync(id);
        if (model == null) return NotFound();

        _context.CouplerModels.Remove(model);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}