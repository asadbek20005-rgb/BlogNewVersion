using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using Blog.Service.Contracts;
using Blog.Service.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    [HttpPost("account/register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        int? code = await _userService.Register(model);

        if (_userService.IsValid)
        {
            return Ok(code);
        }

        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }

    [HttpPost("account/verify-register")]
    public async Task<IActionResult> VerifyRegister(OtpModel model)
    {
        await _userService.VerifyRegister(model);
        if (_userService.IsValid)
        {
            return Ok("Done");
        }
        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }


    [HttpPost("account/login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        int? code = await _userService.Login(model);
        if (_userService.IsValid)
        {
            return Ok(code);
        }
        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }


    [HttpPost("account/verify-login")]
    public async Task<IActionResult> VerifyLogin(OtpModel model)
    {
        string token = await _userService.VerifyLogin(model);
        if (_userService.IsValid)
        {
            return Ok(token);
        }
        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }
}
