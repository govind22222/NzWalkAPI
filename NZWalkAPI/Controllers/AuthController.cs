using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.DTOs;
using NZWalkAPI.Repository.IRepository;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuth _auth;

        public AuthController(UserManager<IdentityUser> userManager, IAuth auth)
        {
            _userManager = userManager;
            _auth = auth;

        }

        [HttpPost]
        [Route("Register")]
        // api/auth/register
        public async Task<IActionResult> Register([FromBody] RegisterDTO regDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = regDto.UserName,
                Email = regDto.UserName
            };
            var identityResponse = await _userManager.CreateAsync(identityUser, regDto.Password);
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


        // id-raghav@gmail.com   pass-raghav@123   Role- WriteRole
        // id-demo@gmail.com   pass-demo@123       Role- ReadeRole
        [HttpPost]
        [Route("Login")]
        // api/auth/login
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginReqDto)
        {
            var user = await _userManager.FindByEmailAsync(loginReqDto.UserId);
            if (user != null)
            {
                var isPassValid = await _userManager.CheckPasswordAsync(user, loginReqDto.Password);
                if (isPassValid)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var jwtToken = _auth.CreateJwtToken(user, roles.ToList());
                    //Create Token after successful validation.
                    var jwtResponse = new LoginResponseDTO
                    {
                        JwtToken = jwtToken
                    };
                    return Ok(jwtResponse);
                }
            }
            return BadRequest("Username or password incorrect !!");
        }
    }
}
