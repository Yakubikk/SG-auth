using System.Security.Claims;
using back_end_9.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ApplicationDbContext context) : ControllerBase
{ 
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await context.Users.FindAsync(userId);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}