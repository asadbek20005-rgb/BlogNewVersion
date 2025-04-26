using Blog.Service.Contracts;

namespace Blog.Service.Services;

public class OtpService : IOtpService
{
    public int GenerateCode()
    {
        return new Random().Next(1111, 9999);
    }
}
