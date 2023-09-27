using Microsoft.AspNetCore.Mvc;
using MinistryTask.Serivices.Abstraction;
using MinistryTask.Serivices.Models.ResposeModels.UserRequestModels;
using MinistryTask.Serivices.Models.ResposeModels.UserResponses;

namespace MinistryTask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequestDto request)
        {
            var result = await _userService.LoginUser(request);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register(UserDto request)
        {
            var result = await _userService.RegisterUserAsync(request);
            return Ok(result);
        }
    }
}

