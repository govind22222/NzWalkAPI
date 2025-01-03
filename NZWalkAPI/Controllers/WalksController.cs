using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models;
using NZWalkAPI.Models.DTOs;
using NZWalkAPI.Repository.IRepository;
using System.Diagnostics;

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
        public async Task<IActionResult> Create([FromBody] AddUpdateWalkDTO adWalkDto)
        {
            //Used auto-mapper to map From WalkDto to Walk Model.
            var walkModel = _mapper.Map<Walk>(adWalkDto);
            if (walkModel == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            walkModel = await _walk.AddWalkAsync(walkModel);
            return Ok(_mapper.Map<WalkDTO>(walkModel));
        }

        //   api/walk/GetAllWalks
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walkModelList = await _walk.GetAllWalksAsync();
            if (walkModelList == null)
            {
                return NotFound();
            }
            var walkDTOList = _mapper.Map<List<WalkDTO>>(walkModelList);
            return Ok(walkDTOList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //  /api/walks/id
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkModel = await _walk.GetWalkByIdAsync(id);
            if (walkModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDTO>(walkModel));
        }

        [HttpPut]
        [Route("{walkId:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid walkId, AddUpdateWalkDTO updateWalkDto)
        {
            if (walkId == Guid.Empty || updateWalkDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var walkModel = await _walk.UpdateWalkAsync(walkId, _mapper.Map<Walk>(updateWalkDto));
            if (walkModel == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<AddUpdateWalkDTO>(walkModel));
            }
        }
        [HttpDelete]
        [Route("{walkId:guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid walkId)
        {
            if (walkId == Guid.Empty)
            {
                return BadRequest();
            }
            var walkData = _walk.DeleteWalkAsync(walkId);
            if (walkData == null)
            {
                return NotFound();
            }
            return Ok(walkData);
        }

    }
}
