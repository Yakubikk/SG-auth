using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class PartInstallation
{
    [Key]
    public Guid InstallationId { get; set; }

    [Required]
    [ForeignKey("Part")]
    public Guid PartId { get; set; }
    public Part Part { get; set; }

    [ForeignKey("RailwayCistern")]
    public Guid? WagonId { get; set; }
    public RailwayCistern? Wagon { get; set; }

    public DateTime InstalledAt { get; set; } = DateTime.UtcNow;

    [MaxLength(100)]
    public string? InstalledBy { get; set; }

    public DateTime? RemovedAt { get; set; }

    [MaxLength(100)]
    public string? RemovedBy { get; set; }

    [ForeignKey("FromLocation")]
    public Guid? FromLocationId { get; set; }
    public Location? FromLocation { get; set; }

    [Required]
    [ForeignKey("ToLocation")]
    public Guid ToLocationId { get; set; }
    public Location ToLocation { get; set; }

    public string? Notes { get; set; }
}