using Kayura.Db.Mutfak.Models;
using Microsoft.Extensions.Logging;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Restaurant entities
/// </summary>
public class RestaurantManager : MutfakManager<Restaurant>
{
  public RestaurantManager(LiteDb<Restaurant> repository, ILogger<RestaurantManager>? logger = null) : base(repository, logger)
  {
  }

  /// <summary>
  /// Creates a new Restaurant instance
  /// </summary>
  public override Restaurant Create()
  {
    var restaurant = new Restaurant
    {
      Name = string.Empty
    };

    return restaurant;
  }

  // CRUD operations
  public override async Task<IEnumerable<Restaurant>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Restaurant?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Restaurant entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Restaurant entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}