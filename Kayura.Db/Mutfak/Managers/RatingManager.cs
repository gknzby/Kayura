using Kayura.Db.Mutfak.Models;
using Microsoft.Extensions.Logging; // Added for ILogger
using System; // Added for DateTime

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Rating entities
/// </summary>
public class RatingManager : MutfakManager<Rating>
{
  public RatingManager(LiteDb<Rating> repository, ILogger<RatingManager>? logger = null) : base(repository, logger)
  {
  }

  /// <summary>
  /// Creates a new Rating instance
  /// </summary>
  public override Rating Create()
  {
    var rating = new Rating
    {
      Title = string.Empty,
      Detail = string.Empty,
      RatingValue = 0,
      Date = DateTime.UtcNow
    };
    return rating;
  }

  // CRUD operations
  public override async Task<IEnumerable<Rating>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Rating?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Rating entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Rating entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}