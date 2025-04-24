using back_end_9.DTOs.Parts;

namespace back_end_9.DTOs.WheelPairs;

public class WheelPairDTO
{
    public Guid PartId { get; set; }
    public decimal? ThicknessLeft { get; set; }
    public decimal? ThicknessRight { get; set; }
    public string WheelType { get; set; }
    public PartDTO Part { get; set; }
}

public class CreateWheelPairDTO
{
    public decimal? ThicknessLeft { get; set; }
    public decimal? ThicknessRight { get; set; }
    public string WheelType { get; set; }
    public CreatePartDTO Part { get; set; }
}

public class UpdateWheelPairDTO
{
    public decimal? ThicknessLeft { get; set; }
    public decimal? ThicknessRight { get; set; }
    public string WheelType { get; set; }
    public UpdatePartDTO Part { get; set; }
}