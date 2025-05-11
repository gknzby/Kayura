using Kayura.Db.Mutfak.Models;

using Microsoft.Extensions.Logging;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Step entities
/// </summary>
public class StepManager : MutfakManager<Step>
{
  public StepManager(LiteDb<Step> repository, ILogger<StepManager>? logger = null) : base(repository, logger)
  {
  }

  /// <summary>
  /// Creates a new Step instance
  /// </summary>
  public override Step Create() => Create(title: string.Empty, detail: string.Empty, note: string.Empty);

  public Step Create(string title, string detail = "", string note = "")
  {
    var step = new Step
    {
      Title = title,
      Detail = detail,
      Note = note
    };
    return step;
  }

  // CRUD operations
  public override async Task<IEnumerable<Step>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Step?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Step entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Step entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}