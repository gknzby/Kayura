using System;
using Microsoft.Extensions.Logging;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Base manager class for Mutfak domain entities
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public abstract class MutfakManager<T> : EntityManager<T> where T : class
{
    protected MutfakManager(LiteDb<T> repository, ILogger<MutfakManager<T>>? logger = null) 
        : base(repository, logger)
    {
    }

    /// <summary>
    /// Creates a new instance of the entity
    /// </summary>
    /// <returns>A new entity instance</returns>
    public abstract T Create();

    /// <summary>
    /// Validates an entity before saving
    /// </summary>
    /// <param name="entity">The entity to validate</param>
    /// <returns>True if valid; otherwise false</returns>
    protected virtual bool Validate(T entity)
    {
        if (entity == null)
        {
            LogError("Cannot validate null entity");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Adds an entity after validation
    /// </summary>
    /// <param name="entity">The entity to add</param>
    public override async Task AddAsync(T entity)
    {
        if (!Validate(entity))
        {
            throw new InvalidOperationException($"Entity of type {typeof(T).Name} failed validation");
        }
        await base.AddAsync(entity);
    }

    /// <summary>
    /// Updates an entity after validation
    /// </summary>
    /// <param name="entity">The entity to update</param>
    public override async Task UpdateAsync(T entity)
    {
        if (!Validate(entity))
        {
            throw new InvalidOperationException($"Entity of type {typeof(T).Name} failed validation");
        }
        await base.UpdateAsync(entity);
    }
}