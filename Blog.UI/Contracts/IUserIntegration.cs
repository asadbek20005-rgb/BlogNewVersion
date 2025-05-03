using Blog.Common.Dtos;
using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using System.Net;

namespace Blog.UI.Contracts;

public interface IUserIntegration
{
    Task<Tuple<HttpStatusCode, string>> Register(RegisterModel model);
    Task<HttpStatusCode> VerifyRegister(OtpModel model);
    Task<Tuple<HttpStatusCode, string>> Login(LoginModel model);
    Task<Tuple<HttpStatusCode,string>> VerifyLogin(OtpModel model);
    Task<Tuple<HttpStatusCode, UserDto?>> GetProfile();
}
