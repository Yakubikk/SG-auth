using System.ComponentModel.DataAnnotations;
using back_end_9.Models.Enums;

namespace back_end_9.Models;

public class Location
{
    [Key]
    public Guid LocationId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public LocationType Type { get; set; }

    public string? Description { get; set; }

    public ICollection<PartInstallation> FromInstallations { get; set; } = new List<PartInstallation>();
    public ICollection<PartInstallation> ToInstallations { get; set; } = new List<PartInstallation>();
}