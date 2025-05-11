using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

[Table("RecipeSteps")]
public class RecipeStep
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Recipe")]
  public int RecipeId { get; set; }
  [Required]
  public virtual required Recipe Recipe { get; set; }

  [ForeignKey("Step")]
  public int StepId { get; set; }
  [Required]
  public virtual required Step Step { get; set; }
}