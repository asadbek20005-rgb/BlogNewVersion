using Blog.Common.Models.User;
using Blog.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace Blog.UI.PageCodeSources;

public class RegisterPageCodeSource : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IUserIntegration UserIntegration { get; set; } 


    public RegisterModel Model { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public async Task Register()
    {
        var statusCode = await UserIntegration.Register(Model);
        if(statusCode == System.Net.HttpStatusCode.OK)
        {

        }
    }
}
