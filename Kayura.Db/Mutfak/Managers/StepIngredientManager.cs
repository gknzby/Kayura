using Kayura.Db.Mutfak.Models;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for StepIngredient entities
/// </summary>
public class StepIngredientManager : MutfakManager<StepIngredient>
{
  private readonly StepManager _stepManager;
  private readonly IngredientManager _ingredientManager;

  public StepIngredientManager(LiteDb<StepIngredient> repository, StepManager stepManager,
      IngredientManager ingredientManager) : base(repository)
  {
    _stepManager = stepManager;
    _ingredientManager = ingredientManager;
  }

  /// <summary>
  /// Not recommended - use Create(Step, Ingredient) instead
  /// </summary>
  public override StepIngredient Create()
  {
    throw new InvalidOperationException("StepIngredient must be created with Step and Ingredient references");
  }

  /// <summary>
  /// Creates a new StepIngredient instance with references to Step and Ingredient
  /// </summary>
  /// <param name="step">Required Step reference</param>
  /// <param name="ingredient">Required Ingredient reference</param>
  public StepIngredient Create(Step step, Ingredient ingredient)
  {
    if (step == null)
      throw new ArgumentNullException(nameof(step));

    if (ingredient == null)
      throw new ArgumentNullException(nameof(ingredient));

    var stepIngredient = new StepIngredient
    {
      Step = step,
      StepId = step.Id,
      Ingredient = ingredient,
      IngredientId = ingredient.Id,
      Amount = 0,
      AmountType = AmountTypes.Pieces
    };

    return stepIngredient;
  }

  // CRUD operations
  public override async Task<IEnumerable<StepIngredient>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<StepIngredient?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(StepIngredient entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(StepIngredient entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}