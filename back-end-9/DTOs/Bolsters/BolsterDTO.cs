using back_end_9.DTOs.Parts;

namespace back_end_9.DTOs.Bolsters;

public class BolsterDTO
{
    public Guid PartId { get; set; }
    public int? ServiceLifeYears { get; set; }
    public DateTime? ExtendedUntil { get; set; }
    public PartDTO Part { get; set; }
}

public class CreateBolsterDTO
{
    public int? ServiceLifeYears { get; set; }
    public DateTime? ExtendedUntil { get; set; }
    public CreatePartDTO Part { get; set; }
}

public class UpdateBolsterDTO
{
    public int? ServiceLifeYears { get; set; }
    public DateTime? ExtendedUntil { get; set; }
    public UpdatePartDTO Part { get; set; }
}