using AutoMapper;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;

namespace NZWalkAPI.DTOModelAutoMappers
{
    public class DtoModelMapper : Profile
    {
        public DtoModelMapper()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddUpdateRegionDTO, Region>().ReverseMap();

            // Added to map from AddWalkDTO to Walk Model vice-versa.
            CreateMap<AddUpdateWalkDTO, Walk>().ReverseMap();
            CreateMap<WalkDTO, Walk>().ReverseMap();
            CreateMap<DifficultyDTO, Difficulty>().ReverseMap();
        }
    }
}
