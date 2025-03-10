using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.DB;
using NZWalkAPI.Filters;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;
using NZWalkAPI.Repository.IRepository;
using System.Text.Json;

namespace NZWalkAPI.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    // Below attribute denotes that RegionsController is type of ApiController
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly AppDBContext _db;
        private readonly IRegions _regions;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;
        public RegionsController(AppDBContext db, IRegions regions, IMapper mapper, ILogger<RegionsController> logger)
        {
            _db = db;
            _regions = regions;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles ="WriteRole")]
        public async Task<IActionResult> GetAllRegion()
        {
            try
            {
                //throw new Exception("This is custom exception by Raghav- ");
                var regionsList = await _regions.GetRegionsAsync();
                var regionDto = _mapper.Map<List<RegionDTO>>(regionsList);
                _logger.LogInformation($"Accessed region information: {JsonSerializer.Serialize(regionDto)}");
                return Ok(regionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("{id:guid}")] // Denotes that is will be type of GUID.
        [Authorize(Roles = "ReadRole")]
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
        [Authorize(Roles = "WriteRole")]
        // [FromBody] denotes that AddRegionDTO will be received from body.
        public async Task<IActionResult> CreateRegion([FromBody] AddUpdateRegionDTO addRegionDTO)
        {
            if (addRegionDTO != null)
            {
                // Converting the AddRegionDTO to RegionModel.
                var regionModel = _mapper.Map<Region>(addRegionDTO);
                regionModel = await _regions.AddRegion(regionModel);
                var regDTO = _mapper.Map<RegionDTO>(regionModel);
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
        [Authorize(Roles = "WriteRole")]
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
        [Authorize(Roles = "WriteRole")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _regions.DeleteRegion(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = _mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }


    }
}
