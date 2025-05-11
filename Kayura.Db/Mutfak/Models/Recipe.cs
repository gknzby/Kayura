using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents a recipe for a food.
/// </summary>
[Table("Recipes")]
public class Recipe
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Food")]
  public required int FoodId { get; set; }

  [Required]
  public required virtual Food Food { get; set; }

  /// <summary>Name of the recipe.</summary>
  [Required, MaxLength(100)]
  public required string Name { get; set; }

  /// <summary>Detailed description of the recipe.</summary>
  [MaxLength(2000)]
  public string Detail { get; set; } = string.Empty;

  public virtual ICollection<RecipeHistory> RecipeHistories { get; set; } = new List<RecipeHistory>();

  [InverseProperty("BaseRecipe")]
  public virtual ICollection<SubRecipe> BaseSubRecipes { get; set; } = new List<SubRecipe>();

  [InverseProperty("SubRecipeDetail")]
  public virtual ICollection<SubRecipe> SubRecipes { get; set; } = new List<SubRecipe>();

  public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();

  public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}