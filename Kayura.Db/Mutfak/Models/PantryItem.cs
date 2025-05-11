using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents an item in the pantry.
/// </summary>
[Table("PantryItems")]
public class PantryItem
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Product")]
  public int ProductId { get; set; }
  [Required]
  public required virtual Product Product { get; set; }

  public decimal Quantity { get; set; }
  public DateTime PurchaseDate { get; set; }
  public DateTime ExpirationDate { get; set; }
}