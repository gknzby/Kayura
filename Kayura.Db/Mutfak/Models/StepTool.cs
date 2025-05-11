using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Associates a tool with a step.
/// </summary>
[Table("StepTools")]
public class StepTool
{
  [Key]
  public int Id { get; set; }

  [ForeignKey("Step")]
  public required int StepId { get; set; }

  [Required]
  public virtual required Step Step { get; set; }

  [ForeignKey("Tool")]
  public required int ToolId { get; set; }

  [Required]
  public virtual required Tool Tool { get; set; }
}