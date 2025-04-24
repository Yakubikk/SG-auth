namespace back_end_9.DTOs.Manufacturers;

public class ManufacturerDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
}

public class CreateManufacturerDTO
{
    public string Name { get; set; }
    public string Country { get; set; }
}

public class UpdateManufacturerDTO
{
    public string Name { get; set; }
    public string Country { get; set; }
}