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
        [Route("Register")]
        // api/auth/register
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

        [HttpPost]
        [Route("Login")]
        // api/auth/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginReqDto)
        {
            var user= await _userManager.FindByEmailAsync(loginReqDto.UserId);
            if (user != null)
            {
                var isPassValid = await _userManager.CheckPasswordAsync(user, loginReqDto.Password);
                if (isPassValid) 
                {
                    //Create Token after successful validation.
                    return Ok("Login Successful !!");
                }
            }
            return BadRequest("Username or password incorrect !!");

        }
    }
}
