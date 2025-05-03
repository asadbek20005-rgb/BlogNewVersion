using System.Net.Http.Headers;
using System.Net.Http;
using Blazored.LocalStorage;
using Blog.UI.Contracts;

namespace Blog.UI.Helpers;

public class TokenHelper(HttpClient httpClient, ILocalStorageService localStorageService) : ITokenHelper
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILocalStorageService _localStorageService= localStorageService;
    private const string key = "token";
    public async Task AddTokenToHeader()
    {
        var token = await _localStorageService.GetItemAsync<string>(key);
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("video/mp4"));

        }
    }
}
