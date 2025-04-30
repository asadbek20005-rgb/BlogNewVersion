using Blog.Common.Models.Otp;
using Blog.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace Blog.UI.PageCodeSources;

public class VerifyLoginPageCodeSource : ComponentBase
{
    [Inject] private IUserIntegration UserIntegration { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }

    public OtpModel Model { get; set; } = new();
    [Parameter] public string Code { get;set; }
    public async Task VerifyLogin()
    {
        var (statusCode, token) = await UserIntegration.VerifyLogin(Model);
        if (statusCode == System.Net.HttpStatusCode.OK)
        {
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
