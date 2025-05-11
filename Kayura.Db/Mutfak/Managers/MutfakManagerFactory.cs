using System.Collections.Concurrent;

using Kayura.Db.Mutfak.Models;

using Microsoft.Extensions.Logging;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Factory for creating and managing Mutfak manager instances with support for dependency injection
/// </summary>
public class MutfakManagerFactory(EntityManagerFactory entityManagerFactory, ILoggerFactory? loggerFactory = null) : IDisposable
{
  private readonly ConcurrentDictionary<Type, object> managers = new(); // Use ConcurrentDictionary for thread safety
  private readonly EntityManagerFactory entityManagerFactory = entityManagerFactory ?? throw new ArgumentNullException(nameof(entityManagerFactory));
  private readonly ILoggerFactory? loggerFactory = loggerFactory;
  private bool disposed;

  protected ILogger<T>? CreateLogger<T>() => loggerFactory?.CreateLogger<T>();

  // Generic method to get or create managers (thread-safe)
  private TManager GetOrCreateManager<TManager, TEntity>(Func<TManager> factory)
      where TManager : class
      where TEntity : class => (TManager)managers.GetOrAdd(typeof(TManager), _ => factory());

  /// <summary>
  /// Gets or creates a RestaurantManager
  /// </summary>
  public RestaurantManager GetRestaurantManager()
  {
    return GetOrCreateManager<RestaurantManager, Restaurant>(() =>
    {
      LiteDb<Restaurant> repository = entityManagerFactory.GetRepository<Restaurant>();
      ILogger<RestaurantManager>? logger = CreateLogger<RestaurantManager>();
      return new RestaurantManager(repository, logger);
    });
  }

  /// <summary>
  /// Gets or creates an IngredientManager
  /// </summary>
  public IngredientManager GetIngredientManager()
  {
    return GetOrCreateManager<IngredientManager, Ingredient>(() =>
    {
      LiteDb<Ingredient> repository = entityManagerFactory.GetRepository<Ingredient>();
      ILogger<IngredientManager>? logger = CreateLogger<IngredientManager>();
      return new IngredientManager(repository, logger);
    });
  }

  /// <summary>
  /// Gets or creates a FoodManager
  /// </summary>
  public FoodManager GetFoodManager()
  {
    return GetOrCreateManager<FoodManager, Food>(() =>
    {
      LiteDb<Food> repository = entityManagerFactory.GetRepository<Food>();
      ILogger<FoodManager>? logger = CreateLogger<FoodManager>();
      return new FoodManager(repository, logger);
    });
  }

  /// <summary>
  /// Gets or creates a PantryItemManager
  /// </summary>
  public PantryItemManager GetPantryItemManager()
  {
    return GetOrCreateManager<PantryItemManager, PantryItem>(() =>
    {
      LiteDb<PantryItem> repository = entityManagerFactory.GetRepository<PantryItem>();
      ProductManager productManager = GetProductManager();
      ILogger<PantryItemManager>? logger = CreateLogger<PantryItemManager>();
      return new PantryItemManager(repository, productManager, logger);
    });
  }

  /// <summary>
  /// Gets or creates a ProductManager
  /// </summary>
  public ProductManager GetProductManager()
  {
    return GetOrCreateManager<ProductManager, Product>(() =>
    {
      LiteDb<Product> repository = entityManagerFactory.GetRepository<Product>();
      IngredientManager ingredientManager = GetIngredientManager();
      RatingManager ratingManager = GetRatingManager();
      ILogger<ProductManager>? logger = CreateLogger<ProductManager>();
      return new ProductManager(repository, ingredientManager, ratingManager, logger);
    });
  }

  /// <summary>
  /// Gets or creates a RatingManager
  /// </summary>
  public RatingManager GetRatingManager()
  {
    return GetOrCreateManager<RatingManager, Rating>(() =>
    {
      LiteDb<Rating> repository = entityManagerFactory.GetRepository<Rating>();
      ILogger<RatingManager>? logger = CreateLogger<RatingManager>();
      return new RatingManager(repository, logger);
    });
  }

  /// <summary>
  /// Gets or creates a RecipeManager
  /// </summary>
  public RecipeManager GetRecipeManager()
  {
    return GetOrCreateManager<RecipeManager, Recipe>(() =>
    {
      LiteDb<Recipe> repository = entityManagerFactory.GetRepository<Recipe>();
      FoodManager foodManager = GetFoodManager();
      ILogger<RecipeManager>? logger = CreateLogger<RecipeManager>();
      return new RecipeManager(repository, foodManager, logger);
    });
  }

  /// <summary>
  /// Gets or creates a RecipeStepManager
  /// </summary>
  public RecipeStepManager GetRecipeStepManager()
  {
    return GetOrCreateManager<RecipeStepManager, RecipeStep>(() =>
    {
      LiteDb<RecipeStep> repository = entityManagerFactory.GetRepository<RecipeStep>();
      RecipeManager recipeManager = GetRecipeManager();
      StepManager stepManager = GetStepManager();
      ILogger<RecipeStepManager>? logger = CreateLogger<RecipeStepManager>();
      return new RecipeStepManager(repository, recipeManager, stepManager, logger);
    });
  }

  /// <summary>
  /// Gets or creates a RecipeHistoryManager
  /// </summary>
  public RecipeHistoryManager GetRecipeHistoryManager()
  {
    return GetOrCreateManager<RecipeHistoryManager, RecipeHistory>(() =>
    {
      LiteDb<RecipeHistory> repository = entityManagerFactory.GetRepository<RecipeHistory>();
      RecipeManager recipeManager = GetRecipeManager();
      RatingManager ratingManager = GetRatingManager();
      ILogger<RecipeHistoryManager>? logger = CreateLogger<RecipeHistoryManager>();
      return new RecipeHistoryManager(repository, recipeManager, ratingManager, logger);
    });
  }

  /// <summary>
  /// Gets or creates a StepManager
  /// </summary>
  public StepManager GetStepManager()
  {
    return GetOrCreateManager<StepManager, Step>(() =>
    {
      LiteDb<Step> repository = entityManagerFactory.GetRepository<Step>();
      ILogger<StepManager>? logger = CreateLogger<StepManager>();
      return new StepManager(repository, logger);
    });
  }

  /// <summary>
  /// Gets or creates a StepIngredientManager
  /// </summary>
  public StepIngredientManager GetStepIngredientManager()
  {
    return GetOrCreateManager<StepIngredientManager, StepIngredient>(() =>
    {
      LiteDb<StepIngredient> repository = entityManagerFactory.GetRepository<StepIngredient>();
      StepManager stepManager = GetStepManager();
      IngredientManager ingredientManager = GetIngredientManager();
      ILogger<StepIngredientManager>? logger = CreateLogger<StepIngredientManager>();
      return new StepIngredientManager(repository, stepManager, ingredientManager, logger);
    });
  }

  /// <summary>
  /// Gets or creates an OrderManager
  /// </summary>
  public OrderManager GetOrderManager()
  {
    return GetOrCreateManager<OrderManager, Order>(() =>
    {
      LiteDb<Order> repository = entityManagerFactory.GetRepository<Order>();
      RecipeManager recipeManager = GetRecipeManager();
      RestaurantManager restaurantManager = GetRestaurantManager();
      RatingManager ratingManager = GetRatingManager();
      ILogger<OrderManager>? logger = CreateLogger<OrderManager>();
      return new OrderManager(repository, recipeManager, restaurantManager, ratingManager, logger);
    });
  }

  /// <summary>
  /// Gets or creates a ToolManager
  /// </summary>
  public ToolManager GetToolManager()
  {
    return GetOrCreateManager<ToolManager, Tool>(() =>
    {
      LiteDb<Tool> repository = entityManagerFactory.GetRepository<Tool>();
      ILogger<ToolManager>? logger = CreateLogger<ToolManager>();
      return new ToolManager(repository, logger);
    });
  }

  // Add a clean method
  protected virtual void Dispose(bool disposing)
  {
    if (!disposed)
    {
      if (disposing)
      {
        // Dispose managed resources.
        foreach (object managerInstance in managers.Values)
        {
          if (managerInstance is IDisposable disposableManager)
          {
            disposableManager.Dispose();
          }
        }

        managers.Clear(); // Clear the dictionary after disposing all instances.
      }
      // Future: Dispose unmanaged resources here if any are added.
      disposed = true;
    }
  }

  /// <summary>
  /// Disposes resources used by this factory
  /// </summary>
  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }
}