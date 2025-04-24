using AutoMapper;
using back_end_9.Data;
using back_end_9.DTOs.Couplers;
using back_end_9.Models;
using back_end_9.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class CouplersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CouplersController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CouplerDTO>>> GetCouplers()
    {
        var couplers = await _context.Couplers
            .Include(c => c.Part)
            .Include(c => c.CouplerModel)
            .ToListAsync();
        return Ok(_mapper.Map<IEnumerable<CouplerDTO>>(couplers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CouplerDTO>> GetCoupler(Guid id)
    {
        var coupler = await _context.Couplers
            .Include(c => c.Part)
            .Include(c => c.CouplerModel)
            .FirstOrDefaultAsync(c => c.PartId == id);
        if (coupler == null) return NotFound();
        return _mapper.Map<CouplerDTO>(coupler);
    }

    [HttpPost]
    public async Task<ActionResult<CouplerDTO>> CreateCoupler(CreateCouplerDTO createDto)
    {
        var coupler = _mapper.Map<Coupler>(createDto);
        coupler.PartId = Guid.NewGuid();
        coupler.Part = _mapper.Map<Part>(createDto.Part);
        coupler.Part.PartId = coupler.PartId;
        coupler.Part.PartType = PartType.coupler;
        coupler.Part.CreatedAt = DateTime.UtcNow;
        
        _context.Couplers.Add(coupler);
        await _context.SaveChangesAsync();

        var couplerDto = _mapper.Map<CouplerDTO>(coupler);
        return CreatedAtAction(nameof(GetCoupler), new { id = couplerDto.PartId }, couplerDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCoupler(Guid id, UpdateCouplerDTO updateDto)
    {
        var coupler = await _context.Couplers
            .Include(c => c.Part)
            .FirstOrDefaultAsync(c => c.PartId == id);
        if (coupler == null) return NotFound();

        _mapper.Map(updateDto, coupler);
        _mapper.Map(updateDto.Part, coupler.Part);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupler(Guid id)
    {
        var coupler = await _context.Couplers.FindAsync(id);
        if (coupler == null) return NotFound();

        _context.Couplers.Remove(coupler);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}