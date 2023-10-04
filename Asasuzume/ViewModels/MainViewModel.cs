using Asasuzume.Models;
using System.Collections.ObjectModel;

namespace Asasuzume.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        _items.Add(new() { ImagePath = "Bamboo5" });
    }

    private readonly ObservableCollection<MahjongTile> _items = new();

}
