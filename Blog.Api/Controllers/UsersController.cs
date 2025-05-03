using Blog.Api.Filters;
using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using Blog.Service.Contracts;
using Blog.Service.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService, IUserHelper userHelper) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IUserHelper _userHelper = userHelper;
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
    [ServiceFilter(typeof(OtpVerificationFilter))]
    [HttpPost("account/verify-register")]
    public async Task<IActionResult> VerifyRegister(OtpModel model)
    {
        await _userService.VerifyRegister();
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

    [ServiceFilter(typeof(OtpVerificationFilter))]
    [HttpPost("account/verify-login")]
    public async Task<IActionResult> VerifyLogin(OtpModel model)
    {
        string token = await _userService.VerifyLogin();
        if (_userService.IsValid)
        {
            return Ok(token);
        }
        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }


    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var id = _userHelper.GetUserId();
        var user = await _userService.GetProfile(id);
        if (_userService.IsValid)
        {
            return Ok(user);
        }
        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }

    [HttpPost("profile/upload-picture")]
    [Authorize]
    public async Task<IActionResult> UploadProfilePicture(IFormFile file)
    {
        var id = _userHelper.GetUserId();
        string path = await _userService.UploadProfilePicture(id, file);
        if (_userService.IsValid)
        {
            return Ok(path);
        }
        _userService.CopyToModelState(ModelState);
        return BadRequest(ModelState);
    }
}
