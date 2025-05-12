using Kayura.Db;
using Kayura.Mutfak.UIX.Extensions;
using Kayura.Mutfak.WebApp.Components;
using Kayura.Mutfak.WebApp.Components.Account;
using Kayura.Mutfak.WebApp.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kayura.Mutfak.WebApp;

public class Program
{
  public static void Main(string[] args)
  {
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    _ = builder.Services.AddRazorComponents()
        .AddInteractiveWebAssemblyComponents()
        .AddAuthenticationStateSerialization();

    _ = builder.Services.AddCascadingAuthenticationState();
    _ = builder.Services.AddScoped<IdentityUserAccessor>();
    _ = builder.Services.AddScoped<IdentityRedirectManager>();

    _ = builder.Services.AddAuthentication(options =>
        {
          options.DefaultScheme = IdentityConstants.ApplicationScheme;
          options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddIdentityCookies();
    _ = builder.Services.AddAuthorization();

    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    _ = builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    _ = builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

    _ = builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

    _ = builder.Services.AddKayuraDb();
    _ = builder.Services.AddKayuraViewModels();

    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseWebAssemblyDebugging();
      _ = app.UseMigrationsEndPoint();
    }
    else
    {
      _ = app.UseExceptionHandler("/Error");
      // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
      _ = app.UseHsts();
    }

    _ = app.UseHttpsRedirection();

    _ = app.UseAntiforgery();

    _ = app.MapStaticAssets();
    _ = app.MapRazorComponents<App>()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(
            typeof(Client._Imports).Assembly,
            typeof(Kayura.Mutfak.UIX._Imports).Assembly);  // Add UIX assembly for routing

    // Add additional endpoints required by the Identity /Account Razor components.
    _ = app.MapAdditionalIdentityEndpoints();

    app.Run();
  }
}
