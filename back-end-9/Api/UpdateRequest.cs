namespace back_end_9.Api;

public struct UpdateRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
}
