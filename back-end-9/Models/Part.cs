using System.ComponentModel.DataAnnotations;
using back_end_9.Models.Enums;

namespace back_end_9.Models;

public class Part
{
    [Key]
    public Guid PartId { get; set; }

    [Required]
    public PartType PartType { get; set; }

    [Required]
    [MaxLength(20)]
    public string StampNumber { get; set; }

    [MaxLength(50)]
    public string? SerialNumber { get; set; }

    public int? ManufactureYear { get; set; }

    [MaxLength(100)]
    public string? CurrentLocation { get; set; }

    public PartStatus Status { get; set; } = PartStatus.active;

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Навигационные свойства для специализированных деталей
    public WheelPair? WheelPair { get; set; }
    public SideFrame? SideFrame { get; set; }
    public Bolster? Bolster { get; set; }
    public Coupler? Coupler { get; set; }
    public ShockAbsorber? ShockAbsorber { get; set; }
    public ICollection<PartInstallation> Installations { get; set; }
}
