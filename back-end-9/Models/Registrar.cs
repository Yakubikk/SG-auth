using System.ComponentModel.DataAnnotations;

namespace back_end_9.Models;

public class Registrar
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}