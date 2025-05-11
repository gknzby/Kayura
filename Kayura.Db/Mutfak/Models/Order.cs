using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents an order placed at a restaurant for a recipe.
/// </summary>
[Table("Orders")]
public class Order
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Recipe")]
  public required int RecipeId { get; set; }

  [Required]
  public required virtual Recipe Recipe { get; set; }

  [ForeignKey("Restaurant")]
  public required int RestaurantId { get; set; }

  [Required]
  public required virtual Restaurant Restaurant { get; set; }

  public decimal Price { get; set; }

  [ForeignKey("Rating")]
  public int? RatingId { get; set; }
  public virtual Rating? Rating { get; set; }
}