using API.DTOs;
using AutoMapper;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;        
        private readonly IMapper _mapper;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IMapper mapper)
        {
            _mapper = mapper;            
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                lockoutOnFailure:false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                // Generate a JWT token with user claims
                var tokenClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Name, user.DisplayName),
                    new Claim(ClaimTypes.Email, user.Email),
                    // Add more claims as needed
                };

                //var token = _tokenService.CreateToken(user, tokenClaims);
                return Ok();
            }
          
            if (result.RequiresTwoFactor)
            {
                // Handle two-factor authentication if enabled
                // For example, send a code to the user's phone
                return BadRequest(new { Message = "Two-factor authentication required" });
            }

            return BadRequest(new { Message = "Invalid login attempt" });

        }



        //[HttpPost("register")]
        //public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        //{
        //    var user = new ApplicationUser
        //    {
        //        DisplayName = registerDto.DisplayName,
        //        Email = registerDto.Email,
        //        UserName = registerDto.Email
        //    };
        //    var result = await _userManager.CreateAsync(user, registerDto.Password);
        //    if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        //    return new UserDto
        //    {
        //        Email = user.Email,
        //        Token = _tokenService.CreateToken(user),
        //        DisplayName = user.DisplayName
        //    };
        //}
        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<UserDto>> GetCurrentUser()
        //{
        //    var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
        //    return new UserDto
        //    {
        //        Email = user.Email,
        //        Token = _tokenService.CreateToken(user),
        //        DisplayName = user.DisplayName
        //    };
        //}
    }
}
