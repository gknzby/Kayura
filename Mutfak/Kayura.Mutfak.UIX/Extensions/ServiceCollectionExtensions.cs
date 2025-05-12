using Kayura.Mutfak.UIX.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace Kayura.Mutfak.UIX.Extensions;

/// <summary>
/// Extensions for configuring Kayura.Mutfak.UIX services with dependency injection
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Adds Kayura.Mutfak.UIX ViewModels to the service collection
  /// </summary>
  /// <param name="services">The service collection</param>
  /// <returns>The service collection for chaining</returns>
  public static IServiceCollection AddKayuraViewModels(this IServiceCollection services)
  {
    // Register ViewModels
    _ = services.AddScoped<FoodsVM>();

    // Add other ViewModels as needed

    return services;
  }
}