using AutoMapper;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;

namespace NZWalkAPI.DTOModelAutoMappers
{
    public class DtoModelMapper :Profile
    {
        public DtoModelMapper()
        {
            CreateMap<Region, RegionDTO>().ReverseMap(); 
        }
    }
}
