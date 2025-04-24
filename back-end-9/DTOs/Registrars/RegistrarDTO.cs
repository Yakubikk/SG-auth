namespace back_end_9.DTOs.Registrars;

public class RegistrarDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class CreateRegistrarDTO
{
    public string Name { get; set; }
}

public class UpdateRegistrarDTO
{
    public string Name { get; set; }
}