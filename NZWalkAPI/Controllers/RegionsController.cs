using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalkAPI.DB;
using NZWalkAPI.Filters;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;
using NZWalkAPI.Repository.IRepository;

namespace NZWalkAPI.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    // Below attribute denotes that RegionsController is type of ApiController
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AppDBContext _db;
        private readonly IRegions _regions;
        private readonly IMapper _mapper;
        public RegionsController(AppDBContext db, IRegions regions, IMapper mapper)
        {
            _db = db;
            _regions = regions;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegion()
        {
            var regionsList = await _regions.GetRegionsAsync();
            var regionDto= _mapper.Map<List<RegionDTO>>(regionsList);
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:guid}")] // Denotes that is will be type of GUID.
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)  //[FromRoute] denotes that guid will be received from  route
        {
            var region = await _regions.GetRegionById(id);
            if (region == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<RegionDTO>(region));
            }
        }

        [HttpPost]
        [ModelValidationFilter]
        // [FromBody] denotes that AddRegionDTO will be received from body.
        public async Task<IActionResult> CreateRegion([FromBody] AddUpdateRegionDTO addRegionDTO)
        {
            if (addRegionDTO != null)
            {
                // Converting the AddRegionDTO to RegionModel.
                var regionModel = _mapper.Map<Region>(addRegionDTO);
                regionModel = await _regions.AddRegion(regionModel);
                var regDTO= _mapper.Map<RegionDTO>(regionModel);  
                return CreatedAtAction(nameof(GetRegionById), new { id = regDTO.Id }, regDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] AddUpdateRegionDTO updateRegDTO)
        {
            var regionModel = _mapper.Map<Region>(updateRegDTO);            
             regionModel = await _regions.UpdateRegion(id, regionModel);
            if (regionModel != null)
            {   
                var regDTO = _mapper.Map<RegionDTO>(regionModel);                
                return Ok(regDTO);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _regions.DeleteRegion(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDTO= _mapper.Map<RegionDTO>(region);
            // Code Replaced by above line of Automapper.
            //var regionDTO = new RegionDTO
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            return Ok(regionDTO);
        }


    }
}
