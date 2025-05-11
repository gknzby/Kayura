using Microsoft.Extensions.Logging;

namespace Kayura.Db;

/// <summary>
/// Factory for creating and managing EntityManager instances
/// </summary>
public class EntityManagerFactory(ILoggerFactory? loggerFactory = null)
{
  private readonly Dictionary<Type, object> managers = [];
  private readonly Dictionary<Type, object> repositories = [];
  private readonly ILoggerFactory? loggerFactory = loggerFactory;

  /// <summary>
  /// Creates or retrieves an EntityManager for the specified entity type
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  /// <param name="repository">Optional repository override</param>
  /// <returns>EntityManager instance</returns>
  public EntityManager<T> Create<T>(LiteDb<T>? repository = null) where T : class
  {
    if (managers.TryGetValue(typeof(T), out object? existing))
    {
      return (EntityManager<T>)existing;
    }

    LiteDb<T> repo = repository ?? GetRepository<T>();
    ILogger<EntityManager<T>>? logger = loggerFactory?.CreateLogger<EntityManager<T>>();
    var manager = new EntityManager<T>(repo, logger);
    managers[typeof(T)] = manager;

    return manager;
  }

  /// <summary>
  /// Gets or creates a LiteDb repository for the given entity type
  /// </summary>
  /// <typeparam name="T">Entity type</typeparam>
  /// <returns>LiteDb repository instance</returns>
  public LiteDb<T> GetRepository<T>() where T : class
  {
    if (repositories.TryGetValue(typeof(T), out object? existing))
    {
      return (LiteDb<T>)existing;
    }

    ILogger<LiteDb<T>>? logger = loggerFactory?.CreateLogger<LiteDb<T>>();
    var repo = new LiteDb<T>(logger);
    repositories[typeof(T)] = repo;

    return repo;
  }

  /// <summary>
  /// Disposes all repositories
  /// </summary>
  public void Dispose()
  {
    foreach (object repo in repositories.Values)
    {
      if (repo is IDisposable disposable)
      {
        disposable.Dispose();
      }
    }

    repositories.Clear();
    managers.Clear();
  }
}