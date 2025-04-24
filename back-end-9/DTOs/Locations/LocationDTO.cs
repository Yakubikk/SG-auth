namespace back_end_9.DTOs.Locations;

public class LocationDTO
{
    public Guid LocationId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
}

public class CreateLocationDTO
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
}

public class UpdateLocationDTO
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
}