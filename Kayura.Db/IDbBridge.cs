namespace Kayura.Db;

/// <summary>
/// Defines the contract for database operations on entities
/// </summary>
/// <typeparam name="T">The entity type</typeparam>
public interface IDbBridge<T> where T : class
{
  /// <summary>
  /// Retrieves all entities of type T asynchronously.
  /// </summary>
  /// <returns>A collection of all entities</returns>
  Task<IEnumerable<T>> GetAllAsync();

  /// <summary>
  /// Retrieves an entity by its ID asynchronously.
  /// </summary>
  /// <param name="id">The entity's unique identifier</param>
  /// <returns>The entity if found; otherwise null</returns>
  Task<T?> GetByIdAsync(IObjectId id);

  /// <summary>
  /// Deletes an entity with the specified ID asynchronously.
  /// </summary>
  /// <param name="id">The entity's unique identifier</param>
  Task DeleteAsync(IObjectId id);

  /// <summary>
  /// Inserts or updates an entity asynchronously.
  /// </summary>
  /// <param name="item">The entity to insert or update</param>
  Task UpsertAsync(T item);
}

/// <summary>
/// Represents an object that can provide its ID in various types
/// </summary>
public interface IObjectId
{
  /// <summary>
  /// Gets the ID of the object in the specified type
  /// </summary>
  /// <typeparam name="T">The type of ID to retrieve</typeparam>
  /// <param name="id">The output ID value</param>
  void GetId<T>(out T id) where T : new();
}