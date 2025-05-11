using Kayura.Db.Mutfak.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Factory for creating and managing Mutfak manager instances with support for dependency injection
/// </summary>
public class MutfakManagerFactory : IDisposable
{
    private readonly ConcurrentDictionary<Type, object> _managers = new(); // Use ConcurrentDictionary for thread safety
    private readonly EntityManagerFactory _entityManagerFactory;
    private readonly ILoggerFactory? _loggerFactory;
    private bool _disposed;

    public MutfakManagerFactory(EntityManagerFactory entityManagerFactory, ILoggerFactory? loggerFactory = null)
    {
        _entityManagerFactory = entityManagerFactory ?? throw new ArgumentNullException(nameof(entityManagerFactory));
        _loggerFactory = loggerFactory;
    }

    protected ILogger<T>? CreateLogger<T>() => _loggerFactory?.CreateLogger<T>();

    // Generic method to get or create managers (thread-safe)
    private TManager GetOrCreateManager<TManager, TEntity>(Func<TManager> factory) 
        where TManager : class 
        where TEntity : class
    {
        return (TManager)_managers.GetOrAdd(typeof(TManager), _ => factory());
    }

    /// <summary>
    /// Gets or creates a RestaurantManager
    /// </summary>
    public RestaurantManager GetRestaurantManager()
    {
        return GetOrCreateManager<RestaurantManager, Restaurant>(() => {
            var repository = _entityManagerFactory.GetRepository<Restaurant>();
            var logger = CreateLogger<RestaurantManager>();
            return new RestaurantManager(repository, logger);
        });
    }

    /// <summary>
    /// Gets or creates an IngredientManager
    /// </summary>
    public IngredientManager GetIngredientManager()
    {
        return GetOrCreateManager<IngredientManager, Ingredient>(() => {
            var repository = _entityManagerFactory.GetRepository<Ingredient>();
            var logger = CreateLogger<IngredientManager>();
            return new IngredientManager(repository, logger);
        });
    }

    /// <summary>
    /// Gets or creates a FoodManager
    /// </summary>
    public FoodManager GetFoodManager()
    {
        return GetOrCreateManager<FoodManager, Food>(() => {
            var repository = _entityManagerFactory.GetRepository<Food>();
            var logger = CreateLogger<FoodManager>();
            return new FoodManager(repository, logger);
        });
    }

    /// <summary>
    /// Gets or creates a PantryItemManager
    /// </summary>
    public PantryItemManager GetPantryItemManager()
    {
        return GetOrCreateManager<PantryItemManager, PantryItem>(() => {
            var repository = _entityManagerFactory.GetRepository<PantryItem>();
            var productManager = GetProductManager();
            var logger = CreateLogger<PantryItemManager>();
            return new PantryItemManager(repository, productManager, logger);
        });
    }

    /// <summary>
    /// Gets or creates a ProductManager
    /// </summary>
    public ProductManager GetProductManager()
    {
        return GetOrCreateManager<ProductManager, Product>(() => {
            var repository = _entityManagerFactory.GetRepository<Product>();
            var ingredientManager = GetIngredientManager();
            var ratingManager = GetRatingManager();
            var logger = CreateLogger<ProductManager>();
            return new ProductManager(repository, ingredientManager, ratingManager, logger);
        });
    }

    /// <summary>
    /// Gets or creates a RatingManager
    /// </summary>
    public RatingManager GetRatingManager()
    {
        return GetOrCreateManager<RatingManager, Rating>(() => {
            var repository = _entityManagerFactory.GetRepository<Rating>();
            var logger = CreateLogger<RatingManager>();
            return new RatingManager(repository, logger);
        });
    }

    /// <summary>
    /// Gets or creates a RecipeManager
    /// </summary>
    public RecipeManager GetRecipeManager()
    {
        return GetOrCreateManager<RecipeManager, Recipe>(() => {
            var repository = _entityManagerFactory.GetRepository<Recipe>();
            var foodManager = GetFoodManager();
            var logger = CreateLogger<RecipeManager>();
            return new RecipeManager(repository, foodManager, logger);
        });
    }

    /// <summary>
    /// Gets or creates a RecipeStepManager
    /// </summary>
    public RecipeStepManager GetRecipeStepManager()
    {
        return GetOrCreateManager<RecipeStepManager, RecipeStep>(() => {
            var repository = _entityManagerFactory.GetRepository<RecipeStep>();
            var recipeManager = GetRecipeManager();
            var stepManager = GetStepManager();
            var logger = CreateLogger<RecipeStepManager>();
            return new RecipeStepManager(repository, recipeManager, stepManager, logger);
        });
    }

    /// <summary>
    /// Gets or creates a RecipeHistoryManager
    /// </summary>
    public RecipeHistoryManager GetRecipeHistoryManager()
    {
        return GetOrCreateManager<RecipeHistoryManager, RecipeHistory>(() => {
            var repository = _entityManagerFactory.GetRepository<RecipeHistory>();
            var recipeManager = GetRecipeManager();
            var ratingManager = GetRatingManager();
            var logger = CreateLogger<RecipeHistoryManager>();
            return new RecipeHistoryManager(repository, recipeManager, ratingManager, logger);
        });
    }

    /// <summary>
    /// Gets or creates a StepManager
    /// </summary>
    public StepManager GetStepManager()
    {
        return GetOrCreateManager<StepManager, Step>(() => {
            var repository = _entityManagerFactory.GetRepository<Step>();
            var logger = CreateLogger<StepManager>();
            return new StepManager(repository, logger);
        });
    }

    /// <summary>
    /// Gets or creates a StepIngredientManager
    /// </summary>
    public StepIngredientManager GetStepIngredientManager()
    {
        return GetOrCreateManager<StepIngredientManager, StepIngredient>(() => {
            var repository = _entityManagerFactory.GetRepository<StepIngredient>();
            var stepManager = GetStepManager();
            var ingredientManager = GetIngredientManager();
            var logger = CreateLogger<StepIngredientManager>();
            return new StepIngredientManager(repository, stepManager, ingredientManager, logger);
        });
    }

    /// <summary>
    /// Gets or creates an OrderManager
    /// </summary>
    public OrderManager GetOrderManager()
    {
        return GetOrCreateManager<OrderManager, Order>(() => {
            var repository = _entityManagerFactory.GetRepository<Order>();
            var recipeManager = GetRecipeManager();
            var restaurantManager = GetRestaurantManager();
            var ratingManager = GetRatingManager();
            var logger = CreateLogger<OrderManager>();
            return new OrderManager(repository, recipeManager, restaurantManager, ratingManager, logger);
        });
    }

    /// <summary>
    /// Gets or creates a ToolManager
    /// </summary>
    public ToolManager GetToolManager()
    {
        return GetOrCreateManager<ToolManager, Tool>(() => {
            var repository = _entityManagerFactory.GetRepository<Tool>();
            var logger = CreateLogger<ToolManager>();
            return new ToolManager(repository, logger);
        });
    }

    // Add a cleanup method
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources.
                foreach (var managerInstance in _managers.Values)
                {
                    if (managerInstance is IDisposable disposableManager)
                    {
                        disposableManager.Dispose();
                    }
                }
                _managers.Clear(); // Clear the dictionary after disposing all instances.
            }
            // Future: Dispose unmanaged resources here if any are added.
            _disposed = true;
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