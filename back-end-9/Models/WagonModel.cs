using System.ComponentModel.DataAnnotations;

namespace back_end_9.Models;

public class WagonModel
{
    [Key]
    public Guid ModelId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<RailwayCistern> RailwayCisterns { get; set; }
}