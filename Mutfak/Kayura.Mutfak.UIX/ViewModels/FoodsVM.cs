using CommunityToolkit.Mvvm.ComponentModel;

using Kayura.Db.Mutfak.Managers;
using Kayura.Db.Mutfak.Models;

namespace Kayura.Mutfak.UIX.ViewModels;
public partial class FoodsVM(MutfakManagerFactory factory) : MutfakVM(factory)
{
  [ObservableProperty]
  private IEnumerable<Food> foods = [];

  private readonly FoodManager foodMng = factory.GetFoodManager();

}
