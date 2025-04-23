using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class Coupler
{
    [Key]
    [ForeignKey("Part")]
    public Guid PartId { get; set; }
    public Part Part { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}