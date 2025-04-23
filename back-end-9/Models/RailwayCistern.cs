using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class RailwayCistern
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [ForeignKey("Manufacturer")]
    public Guid ManufacturerId { get; set; }
    public Manufacturer Manufacturer { get; set; } = new Manufacturer();
    
    [Required]
    public DateTime BuildDate { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal TareWeight { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal LoadCapacity { get; set; }
    
    [Required]
    public int Length { get; set; }
    
    [Required]
    public int AxleCount { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Volume { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? FillingVolume { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? InitialTareWeight { get; set; }
    
    [Required]
    [ForeignKey("WagonType")]
    public Guid TypeId { get; set; }
    public WagonType WagonType { get; set; } = new WagonType();
    
    [Required]
    [MaxLength(50)]
    public string Model { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(30)]
    public string SerialNumber { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string RegistrationNumber { get; set; } = string.Empty;
    
    [Required]
    public DateTime RegistrationDate { get; set; }
    
    [ForeignKey("Registrar")]
    public Guid? RegistrarId { get; set; }
    public Registrar? Registrar { get; set; }
    
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public Vessel? Vessel { get; set; }
}