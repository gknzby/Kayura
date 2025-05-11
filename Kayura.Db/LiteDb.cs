using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Kayura.Db;

/// <summary>
/// Implementation of IDbBridge for a lightweight database
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class LiteDb<T> : IDbBridge<T>, IDisposable where T : class
{
    private readonly ILogger<LiteDb<T>>? _logger;
    private bool _disposed;

    public LiteDb(ILogger<LiteDb<T>>? logger = null)
    {
        _logger = logger;
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        _logger?.LogDebug("Getting all entities of type {EntityType}", typeof(T).Name);
        throw new NotImplementedException("GetAllAsync implementation needs to be provided");
    }

    public Task<T?> GetByIdAsync(IObjectId id)
    {
        _logger?.LogDebug("Getting entity of type {EntityType} by ID", typeof(T).Name);
        throw new NotImplementedException("GetByIdAsync implementation needs to be provided");
    }

    public Task UpsertAsync(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
            
        _logger?.LogDebug("Upserting entity of type {EntityType}", typeof(T).Name);
        throw new NotImplementedException("UpsertAsync implementation needs to be provided");
    }

    public Task DeleteAsync(IObjectId id)
    {
        if (id == null)
            throw new ArgumentNullException(nameof(id));
            
        _logger?.LogDebug("Deleting entity of type {EntityType}", typeof(T).Name);
        throw new NotImplementedException("DeleteAsync implementation needs to be provided");
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                _logger?.LogDebug("Disposing LiteDb<{EntityType}>", typeof(T).Name);
            }
            
            _disposed = true;
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
