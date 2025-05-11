using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

[Table("RecipeHistories")]
public class RecipeHistory
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Recipe")]
  public int RecipeId { get; set; }
  [Required]
  public required virtual Recipe Recipe { get; set; }

  [ForeignKey("Rating")]
  public int? RatingId { get; set; }
  public virtual Rating? Rating { get; set; }

  public DateTime CreatedDate { get; set; }
}