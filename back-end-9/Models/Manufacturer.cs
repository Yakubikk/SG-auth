using System.ComponentModel.DataAnnotations;

namespace back_end_9.Models;

public class Manufacturer
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Country { get; set; } = string.Empty;
}