using Kayura.Db.Mutfak.Models;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for RecipeStep entities
/// </summary>
public class RecipeStepManager : MutfakManager<RecipeStep>
{
  private readonly RecipeManager _recipeManager;
  private readonly StepManager _stepManager;

  public RecipeStepManager(LiteDb<RecipeStep> repository, RecipeManager recipeManager,
      StepManager stepManager) : base(repository)
  {
    _recipeManager = recipeManager;
    _stepManager = stepManager;
  }

  /// <summary>
  /// Not recommended - use Create(Recipe, Step) instead
  /// </summary>
  public override RecipeStep Create()
  {
    throw new InvalidOperationException("RecipeStep must be created with Recipe and Step references");
  }

  /// <summary>
  /// Creates a new RecipeStep instance with references to Recipe and Step
  /// </summary>
  /// <param name="recipe">Required Recipe reference</param>
  /// <param name="step">Required Step reference</param>
  public RecipeStep Create(Recipe recipe, Step step)
  {
    if (recipe == null)
      throw new ArgumentNullException(nameof(recipe));

    if (step == null)
      throw new ArgumentNullException(nameof(step));

    var recipeStep = new RecipeStep
    {
      Recipe = recipe,
      RecipeId = recipe.Id,
      Step = step,
      StepId = step.Id
    };

    return recipeStep;
  }

  // CRUD operations
  public override async Task<IEnumerable<RecipeStep>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<RecipeStep?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(RecipeStep entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(RecipeStep entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}