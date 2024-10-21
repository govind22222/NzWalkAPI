using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalkAPI.DB;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;

namespace NZWalkAPI.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    // Below attribute denotes that RegionsController is type of ApiController
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly AppDBContext _db;
        public RegionsController(AppDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAllRegion()
        {
            var regionsList = _db.Regions.ToList();
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
        [Route("{id:guid}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var region = _db.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            else
            {
                var regionDto = new RegionDTO()
                {
                    RegionName = region.Name,
                    Code = region.Code,
                    ImageUrl = region.RegionImageUrl
                };
                return Ok(regionDto);
            }
        }

        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionDTO addRegionDTO)
        {
            if (addRegionDTO != null)
            {
                var regionModel = new Region
                {
                    Name = addRegionDTO.Name,
                    Code = addRegionDTO.Code,
                    RegionImageUrl = addRegionDTO.RegionImageUrl
                };
                _db.Regions.Add(regionModel);
                _db.SaveChanges();
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
        [Route("{id:guid}") ]
        public  IActionResult UpdateRegion([FromRoute]Guid id, [FromBody]AddRegionDTO updateRegDTO)
        {
            var region = _db.Regions.FirstOrDefault(x=> x.Id==id);
            if (region!= null)
            {
                //Updating the data that is retrived from the DB.
                region.Name = updateRegDTO.Name;
                region.Code = updateRegDTO.Code;
                region.RegionImageUrl = updateRegDTO.RegionImageUrl;               
                _db.Regions.Update(region);
                _db.SaveChanges();
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
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var region= _db.Regions.FirstOrDefault(x=>x.Id==id);
            if (region != null) 
            {
                _db.Regions.Remove(region);
                _db.SaveChanges();
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
