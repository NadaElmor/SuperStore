using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Entities.User;
using SuperStore.Core.Services.Contracts;
using SuperStore.DTOs;
using SuperStore.Errors;

namespace SuperStore.Controllers
{
   
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(User, loginDto.Password,false);
            if(result.Succeeded is false) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName=User.DisplayName, Email=User.Email,
                Token=await _authService.CreateTokenAsync(User,_userManager)
            });

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split('@')[0],
            };
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded is false) return BadRequest(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _authService.CreateTokenAsync(User, _userManager)
            });
        }
    }
}
