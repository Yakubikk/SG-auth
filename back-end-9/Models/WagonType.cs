using System.ComponentModel.DataAnnotations;

namespace back_end_9.Models;

public class WagonType
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}