using CommunityToolkit.Mvvm.ComponentModel;

using Kayura.Db.Mutfak.Managers;

namespace Kayura.Mutfak.UIX.ViewModels;
public partial class MutfakVM(MutfakManagerFactory factory) : ObservableObject
{
  protected readonly MutfakManagerFactory dbFactory = factory ?? throw new ArgumentNullException(nameof(factory));
}
