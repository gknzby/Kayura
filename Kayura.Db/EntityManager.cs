using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Kayura.Db;

/// <summary>
/// Base manager class for entity operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class EntityManager<T> where T : class
{
    protected readonly LiteDb<T> Repository;
    protected readonly ILogger? Logger;
  
    public EntityManager(LiteDb<T> repository, ILogger<EntityManager<T>>? logger = null)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Logger = logger;
    }

    protected virtual void LogError(string message, Exception? ex = null)
    {
        Logger?.LogError(ex, message);
    }

    protected virtual void LogInfo(string message)
    {
        Logger?.LogInformation(message);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await Repository.GetAllAsync();
        }
        catch (Exception ex)
        {
            LogError($"Error getting all entities of type {typeof(T).Name}", ex);
            throw new DbOperationException($"Failed to retrieve {typeof(T).Name} entities", ex);
        }
    }

    public virtual async Task<T?> GetByIdAsync(IObjectId id)
    {
        if (id == null)
        {
            LogError("GetByIdAsync called with null id");
            throw new ArgumentNullException(nameof(id));
        }
        
        try
        {
            return await Repository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            LogError($"Error getting entity of type {typeof(T).Name} by id", ex);
            throw new DbOperationException($"Failed to retrieve {typeof(T).Name} by ID", ex);
        }
    }

    public virtual async Task AddAsync(T entity)
    {
        if (entity == null)
        {
            LogError("AddAsync called with null entity");
            throw new ArgumentNullException(nameof(entity));
        }
        
        try
        {
            await Repository.UpsertAsync(entity);
            LogInfo($"Entity of type {typeof(T).Name} added successfully");
        }
        catch (Exception ex)
        {
            LogError($"Error adding entity of type {typeof(T).Name}", ex);
            throw new DbOperationException($"Failed to add {typeof(T).Name}", ex);
        }
    }

    public virtual async Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            LogError("UpdateAsync called with null entity");
            throw new ArgumentNullException(nameof(entity));
        }
        
        try
        {
            await Repository.UpsertAsync(entity);
            LogInfo($"Entity of type {typeof(T).Name} updated successfully");
        }
        catch (Exception ex)
        {
            LogError($"Error updating entity of type {typeof(T).Name}", ex);
            throw new DbOperationException($"Failed to update {typeof(T).Name}", ex);
        }
    }

    public virtual async Task DeleteAsync(IObjectId id)
    {
        if (id == null)
        {
            LogError("DeleteAsync called with null id");
            throw new ArgumentNullException(nameof(id));
        }
        
        try
        {
            await Repository.DeleteAsync(id);
            LogInfo($"Entity of type {typeof(T).Name} deleted successfully");
        }
        catch (Exception ex)
        {
            LogError($"Error deleting entity of type {typeof(T).Name}", ex);
            throw new DbOperationException($"Failed to delete {typeof(T).Name}", ex);
        }
    }
}

/// <summary>
/// Exception thrown when a database operation fails
/// </summary>
public class DbOperationException : Exception
{
    public DbOperationException() { }
    public DbOperationException(string message) : base(message) { }
    public DbOperationException(string message, Exception inner) : base(message, inner) { }
}
