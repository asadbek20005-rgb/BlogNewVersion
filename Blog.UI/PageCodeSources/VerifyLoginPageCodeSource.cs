using Blazored.LocalStorage;
using Blog.Common.Models.Otp;
using Blog.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace Blog.UI.PageCodeSources;

public class VerifyLoginPageCodeSource : ComponentBase
{
    [Inject] private IUserIntegration UserIntegration { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }
    [Inject] private ILocalStorageService? LocalStorage { get; set; }

    public OtpModel Model { get; set; } = new();
    [Parameter] public string Code { get; set; }
    public async Task VerifyLogin()
    {
        var (statusCode, token) = await UserIntegration.VerifyLogin(Model);
        if (statusCode == System.Net.HttpStatusCode.OK)
        {
            await LocalStorage!.SetItemAsync("token", token);
            NavigationManager!.NavigateTo("/");
            return;
        }
        else
        {
            // Handle other status codes as needed
            Console.WriteLine("Error: " + statusCode);
        }
    }
}
