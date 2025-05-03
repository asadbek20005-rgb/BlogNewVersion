using Blog.Common.Dtos;
using Blog.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace Blog.UI.PageCodeSources;

public class ProfilePageCodeSource : ComponentBase
{
    [Inject] private IUserIntegration? UserIntegration { get; set; }
    [Inject] private NavigationManager? NavigationManager { get; set; }

    public UserDto Model = new();

    protected override async Task OnInitializedAsync()
    {
        await GetProfile(); 
    }
    public async Task GetProfile()
    {
        var (statusCode, user) = await UserIntegration!.GetProfile();
        if (statusCode == System.Net.HttpStatusCode.OK && user is not null)
        {
            Model = user;
            return;
        }
        else
        {
            // Handle other status codes as needed
            Console.WriteLine("Error: " + statusCode);
        }
    }
}
