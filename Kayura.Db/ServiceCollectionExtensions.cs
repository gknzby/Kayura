using Kayura.Db.Mutfak.Managers;

using Microsoft.Extensions.DependencyInjection;

namespace Kayura.Db;

/// <summary>
/// Extensions for configuring Kayura.Db services with dependency injection
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Adds Kayura.Db services to the service collection
  /// </summary>
  /// <param name="services">The service collection</param>
  /// <returns>The service collection for chaining</returns>
  public static IServiceCollection AddKayuraDb(this IServiceCollection services)
  {
    // Register base services
    _ = services.AddSingleton<EntityManagerFactory>();

    // Register Mutfak-specific services
    _ = services.AddSingleton<MutfakManagerFactory>();

    // Register individual managers
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetIngredientManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRestaurantManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetToolManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetFoodManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetPantryItemManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetProductManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRatingManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRecipeManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRecipeStepManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRecipeHistoryManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetStepManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetStepIngredientManager());
    _ = services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetOrderManager());

    return services;
  }
}
