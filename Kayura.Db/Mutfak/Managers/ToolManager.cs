using Kayura.Db.Mutfak.Models;
using Microsoft.Extensions.Logging;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Tool entities
/// </summary>
public class ToolManager : MutfakManager<Tool>
{
  public ToolManager(LiteDb<Tool> repository, ILogger<ToolManager>? logger = null) : base(repository, logger)
  {
  }

  /// <summary>
  /// Creates a new Tool instance
  /// </summary>
  public override Tool Create()
  {
    var tool = new Tool
    {
      Name = string.Empty
    };

    return tool;
  }

  // CRUD operations
  public override async Task<IEnumerable<Tool>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Tool?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Tool entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Tool entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}