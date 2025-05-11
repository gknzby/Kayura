using Kayura.Db.Mutfak.Models;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Product entities
/// </summary>
public class ProductManager : MutfakManager<Product>
{
  private readonly IngredientManager _ingredientManager;
  private readonly RatingManager _ratingManager;

  public ProductManager(LiteDb<Product> repository, IngredientManager ingredientManager,
      RatingManager ratingManager) : base(repository)
  {
    _ingredientManager = ingredientManager;
    _ratingManager = ratingManager;
  }

  /// <summary>
  /// Not recommended - use Create(Ingredient, Rating) instead
  /// </summary>
  public override Product Create()
  {
    throw new InvalidOperationException("Product must be created with an Ingredient reference");
  }

  /// <summary>
  /// Creates a new Product instance with references to Ingredient and optional Rating
  /// </summary>
  /// <param name="ingredient">Required Ingredient reference</param>
  /// <param name="rating">Optional Rating reference</param>
  public Product Create(Ingredient ingredient, Rating? rating = null)
  {
    if (ingredient == null)
      throw new ArgumentNullException(nameof(ingredient));

    var product = new Product
    {
      Name = string.Empty,
      Ingredient = ingredient,
      IngredientId = ingredient.Id,
      Price = 0
    };

    if (rating != null)
    {
      product.Rating = rating;
      product.RatingId = rating.Id;
    }

    return product;
  }

  // CRUD operations
  public override async Task<IEnumerable<Product>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Product?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Product entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Product entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}