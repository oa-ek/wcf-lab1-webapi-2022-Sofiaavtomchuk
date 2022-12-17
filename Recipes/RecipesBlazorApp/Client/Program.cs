using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RecipesBlazorApp.Client;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped<HttpCurrencyPairService>();
//builder.Services.AddScoped<HttpRequestService>();
//builder.Services.AddScoped<HttpStatusService>();

builder.Services.AddSyncfusionBlazor();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzcyOTI3QDMyMzAyZTMzMmUzMEtNUU81MmxwMmt5S1Mzd2ZjTkVFTlpNMmNvdCtJMGwzSlA5VFpDejYzeEk9");


await builder.Build().RunAsync();
