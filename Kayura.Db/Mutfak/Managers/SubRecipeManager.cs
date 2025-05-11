using Kayura.Db.Mutfak.Models;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for SubRecipe entities
/// </summary>
public class SubRecipeManager : MutfakManager<SubRecipe>
{
  private readonly RecipeManager _recipeManager;

  public SubRecipeManager(LiteDb<SubRecipe> repository, RecipeManager recipeManager) : base(repository)
  {
    _recipeManager = recipeManager;
  }

  /// <summary>
  /// Not recommended - use Create(Recipe, Recipe) instead
  /// </summary>
  public override SubRecipe Create()
  {
    throw new InvalidOperationException("SubRecipe must be created with references to two Recipe items");
  }

  /// <summary>
  /// Creates a new SubRecipe instance with references to base and sub Recipe items
  /// </summary>
  /// <param name="baseRecipe">Required base Recipe reference</param>
  /// <param name="subRecipe">Required sub Recipe reference</param>
  public SubRecipe Create(Recipe baseRecipe, Recipe subRecipe)
  {
    if (baseRecipe == null)
      throw new ArgumentNullException(nameof(baseRecipe));

    if (subRecipe == null)
      throw new ArgumentNullException(nameof(subRecipe));

    var subRecipeItem = new SubRecipe
    {
      BaseRecipe = baseRecipe,
      BaseRecipeId = baseRecipe.Id,
      SubRecipeDetail = subRecipe,
      SubRecipeId = subRecipe.Id
    };

    return subRecipeItem;
  }

  // CRUD operations
  public override async Task<IEnumerable<SubRecipe>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<SubRecipe?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(SubRecipe entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(SubRecipe entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}