namespace back_end_9.Models;

using System.ComponentModel.DataAnnotations;

public class CouplerModel
{
    [Key] 
    public Guid Id { get; set; }

    [Required] 
    [MaxLength(50)] 
    public string Name { get; set; }

    public ICollection<Coupler> Couplers { get; set; }
}