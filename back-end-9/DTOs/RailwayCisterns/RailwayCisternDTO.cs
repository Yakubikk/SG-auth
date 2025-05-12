using back_end_9.DTOs.WagonModel;

namespace back_end_9.DTOs.RailwayCisterns;

public class RailwayCisternDTO
{
    public Guid Id { get; set; }
    public Guid ManufacturerId { get; set; }
    public DateTime BuildDate { get; set; }
    public decimal TareWeight { get; set; }
    public decimal LoadCapacity { get; set; }
    public int Length { get; set; }
    public int AxleCount { get; set; }
    public decimal Volume { get; set; }
    public decimal? FillingVolume { get; set; }
    public decimal? InitialTareWeight { get; set; }
    public Guid TypeId { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public DateTime RegistrationDate { get; set; }
    public Guid? RegistrarId { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Guid? ModelId { get; set; }
    public WagonModelDTO WagonModel { get; set; }
}

public class CreateRailwayCisternDTO
{
    public Guid ManufacturerId { get; set; }
    public DateTime BuildDate { get; set; }
    public decimal TareWeight { get; set; }
    public decimal LoadCapacity { get; set; }
    public int Length { get; set; }
    public int AxleCount { get; set; }
    public decimal Volume { get; set; }
    public decimal? FillingVolume { get; set; }
    public decimal? InitialTareWeight { get; set; }
    public Guid TypeId { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public DateTime RegistrationDate { get; set; }
    public Guid? RegistrarId { get; set; }
    public string Notes { get; set; }
    
    public Guid? ModelId { get; set; }
}

public class UpdateRailwayCisternDTO
{
    public Guid ManufacturerId { get; set; }
    public DateTime BuildDate { get; set; }
    public decimal TareWeight { get; set; }
    public decimal LoadCapacity { get; set; }
    public int Length { get; set; }
    public int AxleCount { get; set; }
    public decimal Volume { get; set; }
    public decimal? FillingVolume { get; set; }
    public decimal? InitialTareWeight { get; set; }
    public Guid TypeId { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public DateTime RegistrationDate { get; set; }
    public Guid? RegistrarId { get; set; }
    public string Notes { get; set; }
    
    public Guid? ModelId { get; set; }
}