using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using Blog.UI.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace Blog.UI.Integrations;

public class UserIntegration(HttpClient httpClient) : IUserIntegration
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task<Tuple<HttpStatusCode, string>> Register(RegisterModel model)
    {
        string url = "/api/Users/account/register";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return new(response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public async Task<HttpStatusCode> VerifyRegister(OtpModel model)
    {
        var url = "/api/Users/account/verify-register";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return response.StatusCode;
    }
}
