using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NZWalkAPI.DB;
using NZWalkAPI.Models;

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
            //var regionsList = new List<Region>
            //{
            //    new Region { Id = Guid.NewGuid(), Name = "AukLand Region", Code = "AKL", RegionImageUrl = "https://picsum.photos/536/354" },
            //    new Region{ Id= Guid.NewGuid(), Name="Willington", Code="WLT", RegionImageUrl="https://picsum.photos/id/16/367/267" }
            //};

            return Ok(regionsList);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var region = _db.Regions.FirstOrDefault(x=>x.Id==id);
            if (region == null) 
            {
                return NotFound();
            }
            else
            {
                return Ok(region);  
                    
            }
            
        }
    }
}
