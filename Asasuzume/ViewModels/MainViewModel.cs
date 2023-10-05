using Asasuzume.Models;
using Asasuzume.Models.Services;
using Splat;
using System.Collections.ObjectModel;

namespace Asasuzume.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        _player = new();
        var deck = Locator.Current.GetService<IGameManager>()!;
        deck.RegisterPlayer(_player);
        deck.RegisterPlayer(new());
        deck.RegisterPlayer(new());
        deck.RegisterPlayer(new());
    }

    public ObservableCollection<MahjongTile> Items => _player.Deck;

    private Player _player;
}
