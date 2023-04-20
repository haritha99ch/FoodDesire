using FoodDesire.Web.Client;
using FoodDesire.Web.Client.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Syncfusion.Blazor;

WebAssemblyHostBuilder? builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
AppConfigurator.ConfigureServices(builder.Services);
builder.Services.AddSyncfusionBlazor();
builder.Services.AddMudServices();
//builder.Services.Configure<PopoverOptions>(options => {
//    options.ThrowOnDuplicateProvider = false;
//});
await builder.Build().RunAsync();
