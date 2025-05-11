using Kayura.Db.Mutfak.Models;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for StepTool entities
/// </summary>
public class StepToolManager : MutfakManager<StepTool>
{
  private readonly StepManager _stepManager;
  private readonly ToolManager _toolManager;

  public StepToolManager(LiteDb<StepTool> repository, StepManager stepManager,
      ToolManager toolManager) : base(repository)
  {
    _stepManager = stepManager;
    _toolManager = toolManager;
  }

  /// <summary>
  /// Not recommended - use Create(Step, Tool) instead
  /// </summary>
  public override StepTool Create()
  {
    throw new InvalidOperationException("StepTool must be created with Step and Tool references");
  }

  /// <summary>
  /// Creates a new StepTool instance with references to Step and Tool
  /// </summary>
  /// <param name="step">Required Step reference</param>
  /// <param name="tool">Required Tool reference</param>
  public StepTool Create(Step step, Tool tool)
  {
    if (step == null)
      throw new ArgumentNullException(nameof(step));

    if (tool == null)
      throw new ArgumentNullException(nameof(tool));

    var stepTool = new StepTool
    {
      Step = step,
      StepId = step.Id,
      Tool = tool,
      ToolId = tool.Id
    };

    return stepTool;
  }

  // CRUD operations
  public override async Task<IEnumerable<StepTool>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<StepTool?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(StepTool entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(StepTool entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}