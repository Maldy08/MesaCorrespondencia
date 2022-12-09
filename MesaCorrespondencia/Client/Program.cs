global using MesaCorrespondencia.Shared;
global using MesaCorrespondencia.Client.Services;
global using MesaCorrespondencia.Client.State;

using Blazored.LocalStorage;
using MesaCorrespondencia.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
//var apiURL = builder.Configuration["AppSettings:apiURL"];
//if (string.IsNullOrEmpty(apiURL)) apiURL = builder.HostEnvironment.BaseAddress;
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOficioService, OficioService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IDeptoService, DeptoService>();
builder.Services.AddScoped<IOficioParametroService, OficioParametroService>();
builder.Services.AddScoped<IGCloudService, GCloudService>();
builder.Services.AddScoped<IFunctionsService, FunctionsService>();
builder.Services.AddSingleton<AppState>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();