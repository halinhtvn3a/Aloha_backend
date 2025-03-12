using AlohaVietnam.Repositories.Models;
using AlohaVietnam.Services.Helper;
using AlohaVietnam.Services.Interfaces;
using AlohaVietnam.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aloha_VietNam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenGenerator _jwtToken;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthService _authService;

        public AuthController(ITokenGenerator jwtToken, UserManager<User> userManager, SignInManager<User> signInManager, IAuthService authService)
        {
            _signInManager = signInManager;
            _jwtToken = jwtToken;
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model);

            if (result.StatusCode == 200)
            {
                return Ok(new { Message = result });

            }
            return StatusCode(result.StatusCode, result);

        }


        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authService.Login(model);

            if (result.StatusCode == 200)
            {
                return Ok(new { Message = result });
            }
            return StatusCode(result.StatusCode, result);

        }


    }
}
