using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents a restaurant.
/// </summary>
[Table("Restaurants")]
public class Restaurant
{
  [Key]
  public int Id { get; set; }

  /// <summary>Name of the restaurant.</summary>
  [Required, MaxLength(100)]
  public required string Name { get; set; }

  public virtual ICollection<Order> Orders { get; set; } = [];
}