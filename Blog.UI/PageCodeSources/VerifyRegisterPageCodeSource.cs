using Blog.Common.Models.Otp;
using Blog.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace Blog.UI.PageCodeSources;

public class VerifyRegisterPageCodeSource : ComponentBase
{
    [Inject] NavigationManager NavigationManager { get; set; }
    = default!;

    [Inject] IUserIntegration UserIntegration { get; set; } = default!;

    public OtpModel Model { get; set; } = new OtpModel();
    [Parameter]
    public string Code { get; set; } = string.Empty;
    
    public async Task VerifyRegister()
    {
        var status = await UserIntegration.VerifyRegister(Model);
        if (status == System.Net.HttpStatusCode.OK)
        {
            NavigationManager.NavigateTo("/account/login");
        }
        else
        {
            // Handle error
        }
    }
}
