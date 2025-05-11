using System.ComponentModel.DataAnnotations;

namespace Kayura.Db.Mutfak.Models;

/// <summary>
/// Base class for all Mutfak entity models
/// </summary>
public abstract class EntityBase : IObjectId
{
    /// <summary>
    /// Primary key for the entity
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets the ID of the object in the specified type
    /// </summary>
    public void GetId<T>(out T id) where T : new()
    {
        if (typeof(T) == typeof(int))
        {
            object boxed = Id;
            id = (T)boxed;
            return;
        }
        
        id = new T();
    }

    /// <summary>
    /// Returns a string representation of the entity
    /// </summary>
    public override string ToString() => $"{GetType().Name} #{Id}";
}
