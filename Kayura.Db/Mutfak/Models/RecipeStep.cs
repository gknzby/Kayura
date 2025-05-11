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
  public required virtual Recipe Recipe { get; set; }

  [ForeignKey("Step")]
  public int StepId { get; set; }
  [Required]
  public required virtual Step Step { get; set; }
}