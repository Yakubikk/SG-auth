using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class WheelPair
{
    [Key]
    [ForeignKey("Part")]
    public Guid PartId { get; set; }
    public Part Part { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? ThicknessLeft { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? ThicknessRight { get; set; }

    [MaxLength(50)]
    public string? WheelType { get; set; }
}