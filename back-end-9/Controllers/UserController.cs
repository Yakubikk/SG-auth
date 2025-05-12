using System.Security.Claims;
using back_end_9.Api;
using back_end_9.Data;
using back_end_9.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end_9.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ApplicationDbContext context, UserManager<User> userManager) : ControllerBase
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

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }
        
        var userRoles = await userManager.GetRolesAsync(user);
        
        var userWithRoles = new 
        {
            user.Id,
            user.UserName,
            user.Email,
            user.PhoneNumber,
            Roles = userRoles
        };

        return Ok(userWithRoles);
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

        var user = await userManager.FindByIdAsync(userId);
        
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

        var existingUser = await userManager.FindByIdAsync(userId);
        
        if (existingUser == null)
        {
            return NotFound();
        }

        existingUser.UserName = updateRequest.UserName;
        existingUser.NormalizedUserName = updateRequest.UserName.ToUpper();
        existingUser.Email = updateRequest.Email;
        existingUser.NormalizedEmail = updateRequest.Email.ToUpper();
        existingUser.PhoneNumber = updateRequest.PhoneNumber;
        
        await userManager.UpdateAsync(existingUser);
        await context.SaveChangesAsync();
        
        return Ok(existingUser);
    }
}
