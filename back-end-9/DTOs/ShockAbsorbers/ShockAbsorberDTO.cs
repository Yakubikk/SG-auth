using back_end_9.DTOs.Parts;

namespace back_end_9.DTOs.ShockAbsorbers;

public class ShockAbsorberDTO
{
    public Guid PartId { get; set; }
    public string Model { get; set; }
    public string ManufacturerCode { get; set; }
    public DateTime? NextRepairDate { get; set; }
    public int? ServiceLifeYears { get; set; }
    public PartDTO Part { get; set; }
}

public class CreateShockAbsorberDTO
{
    public string Model { get; set; }
    public string ManufacturerCode { get; set; }
    public DateTime? NextRepairDate { get; set; }
    public int? ServiceLifeYears { get; set; }
    public CreatePartDTO Part { get; set; }
}

public class UpdateShockAbsorberDTO
{
    public string Model { get; set; }
    public string ManufacturerCode { get; set; }
    public DateTime? NextRepairDate { get; set; }
    public int? ServiceLifeYears { get; set; }
    public UpdatePartDTO Part { get; set; }
}