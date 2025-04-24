namespace back_end_9.DTOs.WagonTypes;

public class WagonTypeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class CreateWagonTypeDTO
{
    public string Name { get; set; }
}

public class UpdateWagonTypeDTO
{
    public string Name { get; set; }
}