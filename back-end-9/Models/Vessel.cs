using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class Vessel
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [ForeignKey("RailwayCistern")]
    public Guid RailwayCisternsId { get; set; }
    public RailwayCistern RailwayCistern { get; set; } = new RailwayCistern();
    
    [MaxLength(30)]
    public string? VesselSerialNumber { get; set; }
    
    public DateTime? VesselBuildDate { get; set; }
}