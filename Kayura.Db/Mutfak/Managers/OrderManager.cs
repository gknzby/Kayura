using Kayura.Db.Mutfak.Models;

namespace Kayura.Db.Mutfak.Managers;

/// <summary>
/// Manager for Order entities
/// </summary>
public class OrderManager : MutfakManager<Order>
{
  private readonly RecipeManager _recipeManager;
  private readonly RestaurantManager _restaurantManager;
  private readonly RatingManager _ratingManager;

  public OrderManager(LiteDb<Order> repository, RecipeManager recipeManager,
      RestaurantManager restaurantManager, RatingManager ratingManager) : base(repository)
  {
    _recipeManager = recipeManager;
    _restaurantManager = restaurantManager;
    _ratingManager = ratingManager;
  }

  /// <summary>
  /// Not recommended - use Create(Recipe, Restaurant, Rating) instead
  /// </summary>
  public override Order Create()
  {
    throw new InvalidOperationException("Order must be created with Recipe and Restaurant references");
  }

  /// <summary>
  /// Creates a new Order instance with references to Recipe and Restaurant
  /// </summary>
  /// <param name="recipe">Required Recipe reference</param>
  /// <param name="restaurant">Required Restaurant reference</param>
  /// <param name="rating">Optional Rating reference</param>
  public Order Create(Recipe recipe, Restaurant restaurant, Rating? rating = null)
  {
    if (recipe == null)
      throw new ArgumentNullException(nameof(recipe));

    if (restaurant == null)
      throw new ArgumentNullException(nameof(restaurant));

    var order = new Order
    {
      Recipe = recipe,
      RecipeId = recipe.Id,
      Restaurant = restaurant,
      RestaurantId = restaurant.Id,
      Price = 0
    };

    if (rating != null)
    {
      order.Rating = rating;
      order.RatingId = rating.Id;
    }

    return order;
  }

  // CRUD operations
  public override async Task<IEnumerable<Order>> GetAllAsync() => await base.GetAllAsync();
  public override async Task<Order?> GetByIdAsync(IObjectId id) => await base.GetByIdAsync(id);
  public override async Task AddAsync(Order entity) => await base.AddAsync(entity);
  public override async Task UpdateAsync(Order entity) => await base.UpdateAsync(entity);
  public override async Task DeleteAsync(IObjectId id) => await base.DeleteAsync(id);
}