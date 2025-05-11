using Microsoft.Extensions.Logging;

namespace Kayura.Db;

/// <summary>
/// Implementation of IDbBridge for a lightweight database
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class LiteDb<T>(ILogger<LiteDb<T>>? logger = null) : IDbBridge<T>, IDisposable where T : class
{
  private readonly ILogger<LiteDb<T>>? logger = logger;
  private bool disposed;

  public Task<IEnumerable<T>> GetAllAsync()
  {
    logger?.LogDebug("Getting all entities of type {EntityType}", typeof(T).Name);
    throw new NotImplementedException("GetAllAsync implementation needs to be provided");
  }

  public Task<T?> GetByIdAsync(IObjectId id)
  {
    logger?.LogDebug("Getting entity of type {EntityType} by ID", typeof(T).Name);
    throw new NotImplementedException("GetByIdAsync implementation needs to be provided");
  }

  public Task UpsertAsync(T item)
  {
    ArgumentNullException.ThrowIfNull(item);

    logger?.LogDebug("Upserting entity of type {EntityType}", typeof(T).Name);
    throw new NotImplementedException("UpsertAsync implementation needs to be provided");
  }

  public Task DeleteAsync(IObjectId id)
  {
    ArgumentNullException.ThrowIfNull(id);

    logger?.LogDebug("Deleting entity of type {EntityType}", typeof(T).Name);
    throw new NotImplementedException("DeleteAsync implementation needs to be provided");
  }

  protected virtual void Dispose(bool disposing)
  {
    if (!disposed)
    {
      if (disposing)
      {
        // Dispose managed resources
        logger?.LogDebug("Disposing LiteDb<{EntityType}>", typeof(T).Name);
      }

      disposed = true;
    }
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }
}
