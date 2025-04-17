using System.Security.Claims;
using back_end_9.Api;
using back_end_9.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ApplicationDbContext context) : ControllerBase
{ 
    [Authorize]
    [HttpGet("check")]
    public IActionResult CheckUser() => Ok();
    
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
    
    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }
    
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        var user = await context.Users.FindAsync(userId);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    [Authorize]
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateRequest updateRequest)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("User ID is required");
        }

        var existingUser = await context.Users.FindAsync(userId);
        
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.UserName = updateRequest.UserName;
        existingUser.NormalizedUserName = updateRequest.UserName.ToUpper();
        existingUser.Email = updateRequest.Email;
        existingUser.PhoneNumber = updateRequest.PhoneNumber;
        
        await context.SaveChangesAsync();
        
        return Ok(existingUser);
    }
}
