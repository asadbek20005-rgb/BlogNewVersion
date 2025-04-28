using Blog.Common.Models.User;
using Blog.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace Blog.UI.PageCodeSources;

public class LoginPageCodeSource : ComponentBase
{
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] IUserIntegration UserIntegration { get; set; } = default!;

    public LoginModel Model { get; set; } = new();

    public async Task Login()
    {
        var result = await UserIntegration.Login(Model);
        if (result.Item1 == System.Net.HttpStatusCode.OK)
        {
            NavigationManager.NavigateTo("/");
        }
        else
        {
            // Handle error
        }
    }

}
