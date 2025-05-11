using Kayura.Db.Mutfak.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Factory for creating and managing Mutfak manager instances with support for dependency injection
/// </summary>
public class MutfakManagerFactory : IDisposable
{
    private readonly Dictionary<Type, object> _managers = new();
    private readonly EntityManagerFactory _entityManagerFactory;
    private readonly ILoggerFactory? _loggerFactory;
    private bool _disposed;

    public MutfakManagerFactory(EntityManagerFactory entityManagerFactory, ILoggerFactory? loggerFactory = null)
    {
        _entityManagerFactory = entityManagerFactory ?? throw new ArgumentNullException(nameof(entityManagerFactory));
        _loggerFactory = loggerFactory;
    }

    protected ILogger<T>? CreateLogger<T>() => _loggerFactory?.CreateLogger<T>();

    // Generic method to get or create managers
    private TManager GetOrCreateManager<TManager, TEntity>(Func<TManager> factory) 
        where TManager : class 
        where TEntity : class
    {
        if (_managers.TryGetValue(typeof(TManager), out var existing))
        {
            return (TManager)existing;
        }

        var manager = factory();
        _managers[typeof(TManager)] = manager;
        return manager;
    }

    /// <summary>
    /// Gets or creates a RestaurantManager
    /// </summary>
    public RestaurantManager GetRestaurantManager()
    {
        return GetOrCreateManager<RestaurantManager, Restaurant>(() => {
            var repository = _entityManagerFactory.GetRepository<Restaurant>();
            return new RestaurantManager(repository);
        });
    }

    /// <summary>
    /// Gets or creates an IngredientManager
    /// </summary>
    public IngredientManager GetIngredientManager()
    {
        return GetOrCreateManager<IngredientManager, Ingredient>(() => {
            var repository = _entityManagerFactory.GetRepository<Ingredient>();
            return new IngredientManager(repository);
        });
    }

    /// <summary>
    /// Gets or creates a FoodManager
    /// </summary>
    public FoodManager GetFoodManager()
    {
        return GetOrCreateManager<FoodManager, Food>(() => {
            var repository = _entityManagerFactory.GetRepository<Food>();
            return new FoodManager(repository);
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
            return new PantryItemManager(repository, productManager);
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
            return new ProductManager(repository, ingredientManager, ratingManager);
        });
    }

    /// <summary>
    /// Gets or creates a RatingManager
    /// </summary>
    public RatingManager GetRatingManager()
    {
        return GetOrCreateManager<RatingManager, Rating>(() => {
            var repository = _entityManagerFactory.GetRepository<Rating>();
            return new RatingManager(repository);
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
            return new RecipeManager(repository, foodManager);
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
            return new RecipeStepManager(repository, recipeManager, stepManager);
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
            return new RecipeHistoryManager(repository, recipeManager, ratingManager);
        });
    }

    /// <summary>
    /// Gets or creates a StepManager
    /// </summary>
    public StepManager GetStepManager()
    {
        return GetOrCreateManager<StepManager, Step>(() => {
            var repository = _entityManagerFactory.GetRepository<Step>();
            return new StepManager(repository);
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
            return new StepIngredientManager(repository, stepManager, ingredientManager);
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
            return new OrderManager(repository, recipeManager, restaurantManager, ratingManager);
        });
    }

    /// <summary>
    /// Gets or creates a ToolManager
    /// </summary>
    public ToolManager GetToolManager()
    {
        return GetOrCreateManager<ToolManager, Tool>(() => {
            var repository = _entityManagerFactory.GetRepository<Tool>();
            return new ToolManager(repository);
        });
    }

    // Add a cleanup method
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Nothing to dispose in this class, but could be needed in the future
            }
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