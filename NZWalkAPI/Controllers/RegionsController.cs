using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalkAPI.DB;
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
        public RegionsController(AppDBContext db, IRegions regions)
        {
            _db = db;
            _regions = regions;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegion()
        {
            var regionsList = await _regions.GetRegionsAsync();
            var regionDto = new List<RegionDTO>();
            foreach (var region in regionsList)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    RegionName = region.Name,
                    Code = region.Code,
                    ImageUrl = region.RegionImageUrl
                });
            }
            //var regionsList = new List<Region>
            //{
            //    new Region { Id = Guid.NewGuid(), Name = "AukLand Region", Code = "AKL", RegionImageUrl = "https://picsum.photos/536/354" },
            //    new Region{ Id= Guid.NewGuid(), Name="Willington", Code="WLT", RegionImageUrl="https://picsum.photos/id/16/367/267" }
            //};
            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id:guid}")] // Denotes that is will be type of GUID.
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)  //[FromRoute] denotes that guid will be received from  route
        {
            var region = await _db.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            else
            {
                var regionDto = new RegionDTO()
                {
                    Id= region.Id,
                    RegionName = region.Name,
                    Code = region.Code,
                    ImageUrl = region.RegionImageUrl
                };
                return Ok(regionDto);
            }
        }

        [HttpPost]
        // [FromBody] denotes that AddRegionDTO will be received from body.
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDTO addRegionDTO)
        {
            if (addRegionDTO != null)
            {
                var regionModel = new Region
                {
                    Name = addRegionDTO.Name,
                    Code = addRegionDTO.Code,
                    RegionImageUrl = addRegionDTO.RegionImageUrl
                };
               await _db.Regions.AddAsync(regionModel);
               await  _db.SaveChangesAsync();
                var regDTO = new RegionDTO()
                {
                    Id = regionModel.Id,
                    RegionName = regionModel.Name,
                    Code = regionModel.Code,
                    ImageUrl = regionModel.RegionImageUrl
                };
                //return Ok();
                Guid regModelId = regionModel.Id;
                Guid regDTOId = regDTO.Id;
                return CreatedAtAction(nameof(GetRegionById), new { id = regDTO.Id }, regDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] AddRegionDTO updateRegDTO)
        {
            var region = await _db.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region != null)
            {
                //Updating the data that is retrived from the DB.
                region.Name = updateRegDTO.Name;
                region.Code = updateRegDTO.Code;
                region.RegionImageUrl = updateRegDTO.RegionImageUrl;
                _db.Regions.Update(region);
                 await _db.SaveChangesAsync();
                var regDTO = new AddRegionDTO
                {
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                };
                return Ok(regDTO);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _db.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region != null)
            {
                _db.Regions.Remove(region);
                await _db.SaveChangesAsync();
                //return Ok(region);

                var regionDTO = new RegionDTO
                {
                    Id = region.Id,
                    RegionName = region.Name,
                    Code = region.Code,
                    ImageUrl = region.RegionImageUrl
                };
                return Ok(regionDTO);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
