using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

[Table("StepIngredients")]
public class StepIngredient
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Step")]
  public int StepId { get; set; }
  [Required]
  public required virtual Step Step { get; set; }

  [ForeignKey("Ingredient")]
  public int IngredientId { get; set; }
  [Required]
  public required virtual Ingredient Ingredient { get; set; }

  public double Amount { get; set; }
  public AmountTypes AmountType { get; set; }
}