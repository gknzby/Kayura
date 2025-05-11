using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents a food item.
/// </summary>
[Table("Foods")]
public class Food
{
  [Key]
  public int Id { get; set; }

  /// <summary>Name of the food item.</summary>
  [Required, MaxLength(100)]
  public required string Name { get; set; }

  public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}