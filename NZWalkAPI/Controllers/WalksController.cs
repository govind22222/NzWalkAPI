using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;
using NZWalkAPI.Repository.IRepository;

namespace NZWalkAPI.Controllers
{
    //   /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalk _walk;
        public WalksController(IWalk walk, IMapper mapper)
        {
            _mapper = mapper;
            _walk = walk;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO adWalkDto)
        {
            //Used auto-mapper to map From WalkDto to Walk Model.
            var walkModel = _mapper.Map<Walk>(adWalkDto);
            if (walkModel == null)
            {
                return BadRequest();
            }
            walkModel = await _walk.AddWalkAsync(walkModel);
            return Ok(_mapper.Map<WalkDTO>(walkModel));
        }

        //   api/walk/GetAllWalks
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkModelList= await _walk.GetAllWalksAsync();
            if (walkModelList == null)
            {
                return NotFound();
            }
            var walkDTOList= _mapper.Map<List<WalkDTO>>(walkModelList);
            return Ok(walkDTOList);
        }
        
        //    GetWalkAsyncById
        //    UpdateWalkAsync
        //    DeleteWalkAsync


    }
}
