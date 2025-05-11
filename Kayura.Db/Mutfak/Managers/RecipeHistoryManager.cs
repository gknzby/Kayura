using Kayura.Db.Mutfak.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for RecipeHistory entities
/// </summary>
public class RecipeHistoryManager : MutfakManager<RecipeHistory>
{
  private readonly RecipeManager _recipeManager;
  private readonly RatingManager _ratingManager;

  public RecipeHistoryManager(LiteDb<RecipeHistory> repository, RecipeManager recipeManager,
      RatingManager ratingManager) : base(repository)
  {
    _recipeManager = recipeManager;
    _ratingManager = ratingManager;
  }

  /// <summary>
  /// Not recommended - use Create(Recipe, Rating) instead
  /// </summary>
  public override RecipeHistory Create()
  {
    throw new InvalidOperationException("RecipeHistory must be created with a Recipe reference");
  }

  /// <summary>
  /// Creates a new RecipeHistory instance with references to Recipe and optional Rating
  /// </summary>
  /// <param name="recipe">Required Recipe reference</param>
  /// <param name="rating">Optional Rating reference</param>
  public RecipeHistory Create(Recipe recipe, Rating? rating = null)
  {
    if (recipe == null)
      throw new ArgumentNullException(nameof(recipe));

    var history = new RecipeHistory
    {
      Recipe = recipe,
      RecipeId = recipe.Id,
      CreatedDate = DateTime.UtcNow
    };

    if (rating != null)
    {
      history.Rating = rating;
      history.RatingId = rating.Id;
    }

    return history;
  }

  // CRUD operations
  public override async Task<IEnumerable<RecipeHistory>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<RecipeHistory?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(RecipeHistory entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(RecipeHistory entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}