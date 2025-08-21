using EventEase;
using EventEase.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Logging.SetMinimumLevel(LogLevel.Debug);

await builder.Build().RunAsync();
