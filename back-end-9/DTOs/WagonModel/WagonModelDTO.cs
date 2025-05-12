using System.ComponentModel.DataAnnotations;

namespace back_end_9.DTOs.WagonModel;

public class WagonModelDTO
{
    public Guid ModelId { get; set; }
    public string Name { get; set; }
}

public class CreateWagonModelDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}

public class UpdateWagonModelDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}