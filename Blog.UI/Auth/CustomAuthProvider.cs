using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.UI.Auth;

public class CustomAuthProvider(ILocalStorageService localStorageService, JwtSecurityTokenHandler jwtSecurityTokenHandler)
       : Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService = localStorageService;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = jwtSecurityTokenHandler;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorageService.GetItemAsync<string>("token");

        if (string.IsNullOrWhiteSpace(token) || !_jwtSecurityTokenHandler.CanReadToken(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = ParseClaimsFromToken(token);
        var identity = new ClaimsIdentity(claims, "jwtAuth");
        var principal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        return new AuthenticationState(principal);
    }

    private List<Claim> ParseClaimsFromToken(string token)
    {
        if (!_jwtSecurityTokenHandler.CanReadToken(token))
        {
            throw new ArgumentException("JWT is not well-formed.");
        }

        var jwtSecurityToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
        var claims = new List<Claim>();

        var userId = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var mobileNumber = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value;

        if (!string.IsNullOrWhiteSpace(userId)) claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
        if (!string.IsNullOrWhiteSpace(mobileNumber)) claims.Add(new Claim(ClaimTypes.MobilePhone, mobileNumber));

        return claims;
    }
}

