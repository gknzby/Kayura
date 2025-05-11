using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents a purchasable product based on an ingredient.
/// </summary>
[Table("Products")]
public class Product
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Ingredient")]
  public required int IngredientId { get; set; }

  [Required]
  public required virtual Ingredient Ingredient { get; set; }

  /// <summary>Name of the product.</summary>
  [Required, MaxLength(100)]
  public required string Name { get; set; }

  public double? Amount { get; set; }

  /// <summary>Type of amount (e.g., grams, cups).</summary>
  public AmountTypes AmountType { get; set; } = AmountTypes.None;

  public decimal? Price { get; set; }

  [ForeignKey("Rating")]
  public int? RatingId { get; set; }
  public virtual Rating? Rating { get; set; }

  public virtual ICollection<PantryItem> PantryItems { get; set; } = new List<PantryItem>();
}