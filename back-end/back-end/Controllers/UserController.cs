using System.Security.Claims;
using back_end.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace back_end.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ApplicationDbContext context) : ControllerBase
{
    [Authorize]
    [HttpGet("check")]
    public IActionResult CheckAuth()
    {
        return Ok();
    }
    
    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        
        return Ok();
    }
}