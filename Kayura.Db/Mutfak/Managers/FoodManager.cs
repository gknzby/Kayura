using Kayura.Db.Mutfak.Models;
using System; // Added for ArgumentException

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Food entities
/// </summary>
public class FoodManager : MutfakManager<Food>
{
  public FoodManager(LiteDb<Food> repository) : base(repository)
  {
  }

  /// <summary>
  /// Creates a new Food instance with default values.
  /// Name will be string.Empty.
  /// </summary>
  public override Food Create()
  {
    LogInfo("Creating a new Food entity with default values.");
    var food = new Food
    {
      Name = string.Empty // Default name
    };
    return food;
  }

  /// <summary>
  /// Creates a new Food instance with the specified name.
  /// </summary>
  /// <param name="name">The name of the food.</param>
  /// <returns>A new Food object.</returns>
  /// <exception cref="ArgumentException">Thrown if the name is null, empty, or whitespace.</exception>
  public Food Create(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
      LogError("Attempted to create Food with null, empty, or whitespace name.");
      throw new ArgumentException("Food name cannot be null, empty, or whitespace.", nameof(name));
    }
    // Consider adding length validation here if desired, e.g., if (name.Length > 100) throw new ArgumentOutOfRangeException(...)
    // However, the model's [MaxLength(100)] attribute will be enforced by the database/ORM layer upon saving.

    LogInfo($"Creating a new Food entity with name: {name}.");
    var food = new Food
    {
      Name = name
    };
    // The caller is responsible for calling AddAsync(food) to persist the entity.
    return food;
  }

  // CRUD operations
  public override async Task<IEnumerable<Food>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Food?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Food entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Food entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}