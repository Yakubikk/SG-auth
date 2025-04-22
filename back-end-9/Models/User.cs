using Microsoft.AspNetCore.Identity;

namespace back_end_9.Models;

public class User : IdentityUser
{
    public ICollection<UserFile> UserFiles { get; set; } = [];
}