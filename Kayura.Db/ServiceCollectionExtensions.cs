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
        services.AddSingleton<EntityManagerFactory>();
        
        // Register Mutfak-specific services
        services.AddSingleton<MutfakManagerFactory>();
        
        // Register individual managers
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetIngredientManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRestaurantManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetToolManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetFoodManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetPantryItemManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetProductManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRatingManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRecipeManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRecipeStepManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetRecipeHistoryManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetStepManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetStepIngredientManager());
        services.AddScoped(sp => sp.GetRequiredService<MutfakManagerFactory>().GetOrderManager());
        
        return services;
    }
}
