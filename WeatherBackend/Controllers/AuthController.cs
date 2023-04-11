using Azure;
using Microsoft.AspNetCore.Mvc;
using ServicesBackend.Model.InputModel;
using ServicesBackend.Services.Base;
using WrapperService.Wrapper;

namespace WeatherBackend.Controllers
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

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserRegisterInputModel model)
        {
            try
            {
                var response = await _userService.RegisterAsync(model);
                var wrapperResult = WrapperResponseService.Wrap<object>(response);
                return Ok(wrapperResult);
            }
            catch (Exception ex)
            {
                var wrapperResult = WrapperResponseService.Wrap<object>(errorMessage: ex.Message);
                return BadRequest(wrapperResult);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginInputModel model)
        {
            try
            {
                var response = await _userService.LoginAsync(model);
                var wrapperResult = WrapperResponseService.Wrap<object>(response);
                return Ok(wrapperResult);
            }
            catch (Exception ex)
            {
                var wrapperResult = WrapperResponseService.Wrap<object>(errorMessage: ex.Message);
                return BadRequest(wrapperResult);
            }
        }
    }
}
