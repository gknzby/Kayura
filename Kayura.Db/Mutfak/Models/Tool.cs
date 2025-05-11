using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Represents a tool used in cooking.
/// </summary>
[Table("Tools")]
public class Tool
{
  [Key]
  public int Id { get; set; }

  /// <summary>Name of the tool.</summary>
  [Required, MaxLength(100)]
  public required string Name { get; set; }

  public virtual ICollection<StepTool> StepTools { get; set; } = new List<StepTool>();
}