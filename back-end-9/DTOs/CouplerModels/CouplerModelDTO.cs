namespace back_end_9.DTOs.CouplerModels;

public class CouplerModelDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class CreateCouplerModelDTO
{
    public string Name { get; set; }
}

public class UpdateCouplerModelDTO
{
    public string Name { get; set; }
}