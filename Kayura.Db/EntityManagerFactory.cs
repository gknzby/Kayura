using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Kayura.Db;

/// <summary>
/// Factory for creating and managing EntityManager instances
/// </summary>
public class EntityManagerFactory
{
    private readonly Dictionary<Type, object> _managers = new();
    private readonly Dictionary<Type, object> _repositories = new();
    private readonly ILoggerFactory? _loggerFactory;

    public EntityManagerFactory(ILoggerFactory? loggerFactory = null)
    {
        _loggerFactory = loggerFactory;
    }

    /// <summary>
    /// Creates or retrieves an EntityManager for the specified entity type
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="repository">Optional repository override</param>
    /// <returns>EntityManager instance</returns>
    public EntityManager<T> Create<T>(LiteDb<T>? repository = null) where T : class
    {
        if (_managers.TryGetValue(typeof(T), out var existing))
        {
            return (EntityManager<T>)existing;
        }
        
        var repo = repository ?? GetRepository<T>();
        var logger = _loggerFactory?.CreateLogger<EntityManager<T>>();
        var manager = new EntityManager<T>(repo, logger);
        _managers[typeof(T)] = manager;
        
        return manager;
    }

    /// <summary>
    /// Gets or creates a LiteDb repository for the given entity type
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <returns>LiteDb repository instance</returns>
    public LiteDb<T> GetRepository<T>() where T : class
    {
        if (_repositories.TryGetValue(typeof(T), out var existing))
        {
            return (LiteDb<T>)existing;
        }
        
        var logger = _loggerFactory?.CreateLogger<LiteDb<T>>();
        var repo = new LiteDb<T>(logger);
        _repositories[typeof(T)] = repo;
        
        return repo;
    }

    /// <summary>
    /// Disposes all repositories
    /// </summary>
    public void Dispose()
    {
        foreach (var repo in _repositories.Values)
        {
            if (repo is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        _repositories.Clear();
        _managers.Clear();
    }
}