using back_end_9.DTOs.CouplerModels;
using back_end_9.DTOs.Parts;

namespace back_end_9.DTOs.Couplers;

public class CouplerDTO
{
    public Guid PartId { get; set; }
    public Guid? ModelId { get; set; }
    public PartDTO Part { get; set; }
    public CouplerModelDTO CouplerModel { get; set; }
}

public class CreateCouplerDTO
{
    public Guid? ModelId { get; set; }
    public CreatePartDTO Part { get; set; }
}

public class UpdateCouplerDTO
{
    public Guid? ModelId { get; set; }
    public UpdatePartDTO Part { get; set; }
}