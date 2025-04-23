using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class SideFrame
{
    [Key]
    [ForeignKey("Part")]
    public Guid PartId { get; set; }
    public Part Part { get; set; }

    public int? ServiceLifeYears { get; set; }
    public DateTime? ExtendedUntil { get; set; }
}