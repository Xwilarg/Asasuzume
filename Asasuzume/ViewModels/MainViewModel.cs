using Asasuzume.Models;
using Asasuzume.Models.Services;
using Splat;
using System.Collections.ObjectModel;

namespace Asasuzume.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        var deck = Locator.Current.GetService<IGameManager>()!;
        _player = new HumanPlayer();
        deck.RegisterPlayer(_player);
        deck.RegisterPlayer(new AIPlayer());
        deck.RegisterPlayer(new AIPlayer());
        deck.RegisterPlayer(new AIPlayer());
    }

    public ObservableCollection<MahjongTile> Items => _player.Deck;

    private HumanPlayer _player;
}
