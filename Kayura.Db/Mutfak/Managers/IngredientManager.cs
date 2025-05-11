using Kayura.Db.Mutfak.Models;

using Microsoft.Extensions.Logging; // Added for ILogger

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Ingredient entities
/// </summary>
public class IngredientManager(LiteDb<Ingredient> repository, ILogger<IngredientManager>? logger = null) : MutfakManager<Ingredient>(repository, logger)
{

  /// <summary>
  /// Creates a new Ingredient instance with default values.
  /// Name will be string.Empty.
  /// </summary>
  public override Ingredient Create()
  {
    LogInfo("Creating a new Ingredient entity with default values.");
    var ingredient = new Ingredient
    {
      Name = string.Empty // Default name
    };
    return ingredient;
  }

  /// <summary>
  /// Creates a new Ingredient instance with the specified name.
  /// </summary>
  /// <param name="name">The name of the ingredient.</param>
  /// <returns>A new Ingredient object.</returns>
  /// <exception cref="ArgumentException">Thrown if the name is null, empty, or whitespace.</exception>
  public Ingredient Create(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
      LogError("Attempted to create Ingredient with null, empty, or whitespace name.");
      throw new ArgumentException("Ingredient name cannot be null, empty, or whitespace.", nameof(name));
    }
    // Consider adding length validation here if desired.
    // Model's [MaxLength(100)] will be enforced by DB/ORM upon saving.

    LogInfo($"Creating a new Ingredient entity with name: {name}.");
    var ingredient = new Ingredient
    {
      Name = name
    };
    // The caller is responsible for calling AddAsync(ingredient) to persist the entity.
    return ingredient;
  }

  // CRUD operations
  public override async Task<IEnumerable<Ingredient>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Ingredient?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Ingredient entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Ingredient entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}