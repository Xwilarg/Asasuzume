using Asasuzume.Models;
using System.Collections.ObjectModel;

namespace Asasuzume.ViewModels;

public class DiscardViewModel : ViewModelBase
{
    public DiscardViewModel()
    { }
    public ObservableCollection<MahjongTile> Items { get; } = new();
}
