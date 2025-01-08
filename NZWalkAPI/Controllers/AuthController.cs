using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.DTOs;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
                _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO regDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = regDto.UserName,
                Email  = regDto.UserName
            };
           var identityResponse= await _userManager.CreateAsync(identityUser, regDto.Password);
            if (identityResponse.Succeeded && regDto.Role != null && regDto.Role.Any())
            {
                identityResponse = await _userManager.AddToRolesAsync(identityUser, regDto.Role);
                if (identityResponse.Succeeded) 
                {
                    return Ok("User created successfully, Please login !!");
                }
            }
            return BadRequest("Error occurred and user not created.");
        }
    }
}
