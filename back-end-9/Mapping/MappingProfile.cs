using AutoMapper;
using back_end_9.DTOs.Bolsters;
using back_end_9.DTOs.CouplerModels;
using back_end_9.DTOs.Couplers;
using back_end_9.DTOs.Locations;
using back_end_9.DTOs.Manufacturers;
using back_end_9.DTOs.PartInstallations;
using back_end_9.DTOs.Parts;
using back_end_9.DTOs.RailwayCisterns;
using back_end_9.DTOs.Registrars;
using back_end_9.DTOs.ShockAbsorbers;
using back_end_9.DTOs.SideFrames;
using back_end_9.DTOs.Vessels;
using back_end_9.DTOs.WagonTypes;
using back_end_9.DTOs.WheelPairs;
using back_end_9.Models;
using back_end_9.Models.Enums;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Manufacturers
        CreateMap<Manufacturer, ManufacturerDTO>().ReverseMap();
        CreateMap<Manufacturer, CreateManufacturerDTO>().ReverseMap();
        CreateMap<Manufacturer, UpdateManufacturerDTO>().ReverseMap();

        // WagonTypes
        CreateMap<WagonType, WagonTypeDTO>().ReverseMap();
        CreateMap<WagonType, CreateWagonTypeDTO>().ReverseMap();
        CreateMap<WagonType, UpdateWagonTypeDTO>().ReverseMap();

        // Registrars
        CreateMap<Registrar, RegistrarDTO>().ReverseMap();
        CreateMap<Registrar, CreateRegistrarDTO>().ReverseMap();
        CreateMap<Registrar, UpdateRegistrarDTO>().ReverseMap();

        // RailwayCisterns
        CreateMap<RailwayCistern, RailwayCisternDTO>()
            .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.Manufacturer.Id))
            .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.WagonType.Id))
            .ForMember(dest => dest.RegistrarId, opt => opt.MapFrom(src => src.Registrar.Id))
            .ReverseMap();
        
        CreateMap<RailwayCistern, CreateRailwayCisternDTO>().ReverseMap();
        CreateMap<RailwayCistern, UpdateRailwayCisternDTO>().ReverseMap();

        // Vessels
        CreateMap<Vessel, VesselDTO>().ReverseMap();
        CreateMap<Vessel, CreateVesselDTO>().ReverseMap();
        CreateMap<Vessel, UpdateVesselDTO>().ReverseMap();

        // Parts
        CreateMap<Part, PartDTO>()
            .ForMember(dest => dest.PartType, opt => opt.MapFrom(src => src.PartType.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.PartType, opt => opt.MapFrom(src => Enum.Parse<PartType>(src.PartType)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PartStatus>(src.Status)));

        CreateMap<Part, CreatePartDTO>()
            .ForMember(dest => dest.PartType, opt => opt.MapFrom(src => src.PartType.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.PartType, opt => opt.MapFrom(src => Enum.Parse<PartType>(src.PartType)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PartStatus>(src.Status)));

        CreateMap<Part, UpdatePartDTO>()
            .ForMember(dest => dest.PartType, opt => opt.MapFrom(src => src.PartType.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.PartType, opt => opt.MapFrom(src => Enum.Parse<PartType>(src.PartType)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PartStatus>(src.Status)));

        // WheelPairs
        CreateMap<WheelPair, WheelPairDTO>()
            .ForMember(dest => dest.Part, opt => opt.MapFrom(src => src.Part))
            .ReverseMap();

        CreateMap<WheelPair, CreateWheelPairDTO>().ReverseMap();
        CreateMap<WheelPair, UpdateWheelPairDTO>().ReverseMap();

        // SideFrames
        CreateMap<SideFrame, SideFrameDTO>()
            .ForMember(dest => dest.Part, opt => opt.MapFrom(src => src.Part))
            .ReverseMap();

        CreateMap<SideFrame, CreateSideFrameDTO>().ReverseMap();
        CreateMap<SideFrame, UpdateSideFrameDTO>().ReverseMap();

        // Bolsters
        CreateMap<Bolster, BolsterDTO>()
            .ForMember(dest => dest.Part, opt => opt.MapFrom(src => src.Part))
            .ReverseMap();

        CreateMap<Bolster, CreateBolsterDTO>().ReverseMap();
        CreateMap<Bolster, UpdateBolsterDTO>().ReverseMap();

        // CouplerModels
        CreateMap<CouplerModel, CouplerModelDTO>().ReverseMap();
        CreateMap<CouplerModel, CreateCouplerModelDTO>().ReverseMap();
        CreateMap<CouplerModel, UpdateCouplerModelDTO>().ReverseMap();

        // Couplers
        CreateMap<Coupler, CouplerDTO>()
            .ForMember(dest => dest.Part, opt => opt.MapFrom(src => src.Part))
            .ForMember(dest => dest.CouplerModel, opt => opt.MapFrom(src => src.CouplerModel))
            .ReverseMap();

        CreateMap<Coupler, CreateCouplerDTO>().ReverseMap();
        CreateMap<Coupler, UpdateCouplerDTO>().ReverseMap();

        // ShockAbsorbers
        CreateMap<ShockAbsorber, ShockAbsorberDTO>()
            .ForMember(dest => dest.Part, opt => opt.MapFrom(src => src.Part))
            .ReverseMap();

        CreateMap<ShockAbsorber, CreateShockAbsorberDTO>().ReverseMap();
        CreateMap<ShockAbsorber, UpdateShockAbsorberDTO>().ReverseMap();

        // Locations
        CreateMap<Location, LocationDTO>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<LocationType>(src.Type)));

        CreateMap<Location, CreateLocationDTO>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<LocationType>(src.Type)));

        CreateMap<Location, UpdateLocationDTO>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<LocationType>(src.Type)));

        // PartInstallations
        CreateMap<PartInstallation, PartInstallationDTO>()
            .ForMember(dest => dest.Part, opt => opt.MapFrom(src => src.Part))
            .ForMember(dest => dest.Wagon, opt => opt.MapFrom(src => src.Wagon))
            .ForMember(dest => dest.FromLocation, opt => opt.MapFrom(src => src.FromLocation))
            .ForMember(dest => dest.ToLocation, opt => opt.MapFrom(src => src.ToLocation))
            .ReverseMap();

        CreateMap<PartInstallation, CreatePartInstallationDTO>().ReverseMap();
        CreateMap<PartInstallation, UpdatePartInstallationDTO>().ReverseMap();
    }
}