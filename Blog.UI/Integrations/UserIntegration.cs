using Blog.Common.Dtos;
using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using Blog.UI.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace Blog.UI.Integrations;

public class UserIntegration(HttpClient httpClient, ITokenHelper tokenHelper) : IUserIntegration
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ITokenHelper _tokenHelper = tokenHelper;

    public async Task<Tuple<HttpStatusCode, UserDto?>> GetProfile()
    {
        await _tokenHelper.AddTokenToHeader();
        string url = "/api/Users/profile";
        var response = await _httpClient.GetAsync(url);
        return new(response.StatusCode, await response.Content.ReadFromJsonAsync<UserDto>());
    }

    public async Task<Tuple<HttpStatusCode, string>> Login(LoginModel model)
    {
        string url = "/api/Users/account/login";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return new(response.StatusCode, await response.Content.ReadAsStringAsync());
    }
    
    public async Task<Tuple<HttpStatusCode, string>> Register(RegisterModel model)
    {
        string url = "/api/Users/account/register";
        var response = await _httpClient.PostAsJsonAsync(url, model);
        return new(response.StatusCode, await response.Content.ReadAsStringAsync());
    }

    public async Task<Tuple<HttpStatusCode, string>> VerifyLogin(OtpModel model)
    {
        string url = "/api/Users/account/verify-login";
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
