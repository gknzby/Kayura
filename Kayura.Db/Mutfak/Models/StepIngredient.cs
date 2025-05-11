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
  public virtual required Step Step { get; set; }

  [ForeignKey("Ingredient")]
  public int IngredientId { get; set; }
  [Required]
  public virtual required Ingredient Ingredient { get; set; }

  public double Amount { get; set; }
  public AmountTypes AmountType { get; set; }
}