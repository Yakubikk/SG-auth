using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_9.Models;

public class Coupler
{
    [Key]
    [ForeignKey("Part")]
    public Guid PartId { get; set; }
    public Part Part { get; set; }

    [ForeignKey("CouplerModel")]
    public Guid? ModelId { get; set; }
    public CouplerModel? CouplerModel { get; set; }
}