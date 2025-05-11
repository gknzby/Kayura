using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents a sub-recipe relationship.
/// </summary>
[Table("SubRecipes")]
public class SubRecipe
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("BaseRecipe")]
  public required int BaseRecipeId { get; set; }

  [Required]
  public virtual required Recipe BaseRecipe { get; set; }

  [ForeignKey("SubRecipeDetail")]
  public required int SubRecipeId { get; set; }

  [Required]
  public virtual required Recipe SubRecipeDetail { get; set; }
}