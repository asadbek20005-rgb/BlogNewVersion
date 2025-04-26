using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using System.Net;

namespace Blog.UI.Contracts;

public interface IUserIntegration
{
    Task<HttpStatusCode> Register(RegisterModel model);
    Task<HttpStatusCode> VerifyRegister(OtpModel model);
}
