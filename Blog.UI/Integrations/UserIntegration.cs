using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using Blog.UI.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace Blog.UI.Integrations;

public class UserIntegration(HttpClient httpClient) : IUserIntegration
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task<HttpStatusCode> Register(RegisterModel model)
    {
        string url = "/api/Users/account/register";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return response.StatusCode;
    }

    public Task<HttpStatusCode> VerifyRegister(OtpModel model)
    {
        throw new NotImplementedException();
    }
}
