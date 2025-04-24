using back_end_9.DTOs.Locations;
using back_end_9.DTOs.Parts;
using back_end_9.DTOs.RailwayCisterns;

namespace back_end_9.DTOs.PartInstallations;

public class PartInstallationDTO
{
    public Guid InstallationId { get; set; }
    public Guid PartId { get; set; }
    public Guid? WagonId { get; set; }
    public DateTime InstalledAt { get; set; }
    public string InstalledBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public string RemovedBy { get; set; }
    public Guid? FromLocationId { get; set; }
    public Guid ToLocationId { get; set; }
    public string Notes { get; set; }
    public PartDTO Part { get; set; }
    public RailwayCisternDTO Wagon { get; set; }
    public LocationDTO FromLocation { get; set; }
    public LocationDTO ToLocation { get; set; }
}

public class CreatePartInstallationDTO
{
    public Guid PartId { get; set; }
    public Guid? WagonId { get; set; }
    public string InstalledBy { get; set; }
    public Guid? FromLocationId { get; set; }
    public Guid ToLocationId { get; set; }
    public string Notes { get; set; }
}

public class UpdatePartInstallationDTO
{
    public Guid PartId { get; set; }
    public Guid? WagonId { get; set; }
    public string InstalledBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public string RemovedBy { get; set; }
    public Guid? FromLocationId { get; set; }
    public Guid ToLocationId { get; set; }
    public string Notes { get; set; }
}