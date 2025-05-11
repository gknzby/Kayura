using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents a user rating for recipes, products, or orders.
/// </summary>
[Table("Ratings")]
public class Rating
{
  [Key]
  public int Id { get; set; }

  /// <summary>Short title for the rating.</summary>
  [Required, MaxLength(100)]
  public required string Title { get; set; }

  /// <summary>Detailed description of the rating.</summary>
  [MaxLength(1000)]
  public string Detail { get; set; } = string.Empty;

  /// <summary>Numeric value of the rating (0-100).</summary>
  [Range(0, 100)]
  public required int RatingValue { get; set; }

  /// <summary>Date the rating was given.</summary>
  public DateTime Date { get; set; }
}