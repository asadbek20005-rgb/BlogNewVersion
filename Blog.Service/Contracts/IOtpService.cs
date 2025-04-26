using Blog.Common.Models.Otp;

namespace Blog.Service.Contracts;

public interface IOtpService
{
    int GenerateCode();
    Task VerifyAsync(OtpModel model);
}
