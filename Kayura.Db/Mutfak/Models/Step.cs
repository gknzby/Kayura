using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents a step in a recipe.
/// </summary>
[Table("Steps")]
public class Step
{
  [Key]
  public int Id { get; set; }

  /// <summary>Title of the step.</summary>
  [Required, MaxLength(100)]
  public required string Title { get; set; }

  /// <summary>Detailed instructions for the step.</summary>
  [MaxLength(2000)]
  public string Detail { get; set; } = string.Empty;

  /// <summary>Additional notes for the step.</summary>
  [MaxLength(500)]
  public string Note { get; set; } = string.Empty;

  public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = [];
  public virtual ICollection<StepTool> StepTools { get; set; } = [];
  public virtual ICollection<StepIngredient> StepIngredients { get; set; } = [];
}