using Kayura.Db.Mutfak.Models;

using Microsoft.Extensions.Logging; // Added for ILogger

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Recipe entities
/// </summary>
public class RecipeManager(LiteDb<Recipe> repository, FoodManager foodManager, ILogger<RecipeManager>? logger = null) : MutfakManager<Recipe>(repository, logger)
{
  private readonly FoodManager foodManager = foodManager ?? throw new ArgumentNullException(nameof(foodManager));

  /// <summary>
  /// Not recommended - use Create(Food) instead
  /// </summary>
  public override Recipe Create() => throw new InvalidOperationException("Recipe must be created with a Food reference");

  /// <summary>
  /// Creates a new Recipe instance with a reference to Food
  /// </summary>
  /// <param name="food">Required Food reference</param>
  public static Recipe Create(Food food)
  {
    ArgumentNullException.ThrowIfNull(food);

    var recipe = new Recipe
    {
      Name = string.Empty,
      Detail = string.Empty,
      Food = food,
      FoodId = food.Id
    };

    return recipe;
  }

  // CRUD operations
  public override async Task<IEnumerable<Recipe>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Recipe?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Recipe entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Recipe entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}