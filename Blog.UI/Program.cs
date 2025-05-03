using Blazored.LocalStorage;
using Blog.UI;
using Blog.UI.Contracts;
using Blog.UI.Helpers;
using Blog.UI.Integrations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7234") });
builder.Services.AddScoped<IUserIntegration, UserIntegration>();
builder.Services.AddScoped<ITokenHelper, TokenHelper>();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
