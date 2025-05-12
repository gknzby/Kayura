using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Kayura.Mutfak.WebApp.Client;

class Program
{
  static async Task Main(string[] args)
  {
    var builder = WebAssemblyHostBuilder.CreateDefault(args);

    _ = builder.Services.AddAuthorizationCore();
    _ = builder.Services.AddCascadingAuthenticationState();
    _ = builder.Services.AddAuthenticationStateDeserialization();

    await builder.Build().RunAsync();
  }
}
