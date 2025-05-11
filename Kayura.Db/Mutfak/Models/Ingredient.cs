using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents an ingredient.
/// </summary>
[Table("Ingredients")]
public class Ingredient
{
  [Key]
  public int Id { get; set; }

  /// <summary>Name of the ingredient.</summary>
  [Required, MaxLength(100)]
  public required string Name { get; set; }

  public virtual ICollection<StepIngredient> StepIngredients { get; set; } = [];
  public virtual ICollection<Product> Products { get; set; } = [];
}