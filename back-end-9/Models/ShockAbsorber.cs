using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class ShockAbsorber
{
    [Key]
    [ForeignKey("Part")]
    public Guid PartId { get; set; }
    public Part Part { get; set; }

    [MaxLength(50)]
    public string? Model { get; set; }

    [MaxLength(20)]
    public string? ManufacturerCode { get; set; }

    public DateTime? NextRepairDate { get; set; }
    public int? ServiceLifeYears { get; set; }
}