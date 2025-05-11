using Kayura.Db.Mutfak.Models;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Recipe entities
/// </summary>
public class RecipeManager : MutfakManager<Recipe>
{
  private readonly FoodManager _foodManager;

  public RecipeManager(LiteDb<Recipe> repository, FoodManager foodManager) : base(repository)
  {
    _foodManager = foodManager;
  }

  /// <summary>
  /// Not recommended - use Create(Food) instead
  /// </summary>
  public override Recipe Create()
  {
    throw new InvalidOperationException("Recipe must be created with a Food reference");
  }

  /// <summary>
  /// Creates a new Recipe instance with a reference to Food
  /// </summary>
  /// <param name="food">Required Food reference</param>
  public Recipe Create(Food food)
  {
    if (food == null)
      throw new ArgumentNullException(nameof(food));

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