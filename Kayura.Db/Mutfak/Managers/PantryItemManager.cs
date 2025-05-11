using Kayura.Db.Mutfak.Models;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for PantryItem entities
/// </summary>
public class PantryItemManager : MutfakManager<PantryItem>
{
  private readonly ProductManager _productManager;

  public PantryItemManager(LiteDb<PantryItem> repository, ProductManager productManager) : base(repository)
  {
    _productManager = productManager;
  }

  /// <summary>
  /// Not recommended - use Create(Product) instead
  /// </summary>
  public override PantryItem Create()
  {
    throw new InvalidOperationException("PantryItem must be created with a Product reference");
  }

  /// <summary>
  /// Creates a new PantryItem instance with a reference to Product
  /// </summary>
  /// <param name="product">Required Product reference</param>
  public PantryItem Create(Product product)
  {
    if (product == null)
      throw new ArgumentNullException(nameof(product));

    var pantryItem = new PantryItem
    {
      Product = product,
      ProductId = product.Id,
      Quantity = 0,
      PurchaseDate = DateTime.Now,
      ExpirationDate = DateTime.Now.AddMonths(1)
    };

    return pantryItem;
  }

  // CRUD operations
  public override async Task<IEnumerable<PantryItem>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<PantryItem?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(PantryItem entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(PantryItem entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}