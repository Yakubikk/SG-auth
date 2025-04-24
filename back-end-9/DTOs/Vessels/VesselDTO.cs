namespace back_end_9.DTOs.Vessels;

public class VesselDTO
{
    public Guid Id { get; set; }
    public Guid RailwayCisternsId { get; set; }
    public string VesselSerialNumber { get; set; }
    public DateTime? VesselBuildDate { get; set; }
}

public class CreateVesselDTO
{
    public Guid RailwayCisternsId { get; set; }
    public string VesselSerialNumber { get; set; }
    public DateTime? VesselBuildDate { get; set; }
}

public class UpdateVesselDTO
{
    public Guid RailwayCisternsId { get; set; }
    public string VesselSerialNumber { get; set; }
    public DateTime? VesselBuildDate { get; set; }
}