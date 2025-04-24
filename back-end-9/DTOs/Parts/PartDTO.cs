namespace back_end_9.DTOs.Parts;

public class PartDTO
{
    public Guid PartId { get; set; }
    public string PartType { get; set; }
    public string StampNumber { get; set; }
    public string SerialNumber { get; set; }
    public int? ManufactureYear { get; set; }
    public string CurrentLocation { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreatePartDTO
{
    public string PartType { get; set; }
    public string StampNumber { get; set; }
    public string SerialNumber { get; set; }
    public int? ManufactureYear { get; set; }
    public string CurrentLocation { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
}

public class UpdatePartDTO
{
    public string PartType { get; set; }
    public string StampNumber { get; set; }
    public string SerialNumber { get; set; }
    public int? ManufactureYear { get; set; }
    public string CurrentLocation { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
}