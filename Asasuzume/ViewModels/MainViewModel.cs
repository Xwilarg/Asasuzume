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
        Human = new HumanPlayer();
        Players = new APlayer[] { Human, new AIPlayer(), new AIPlayer(), new AIPlayer() };
        foreach (var p in Players)
        {
            deck.RegisterPlayer(p);
        }
    }

    public ObservableCollection<MahjongTile> Items => Human.Deck;

    private HumanPlayer Human { init; get; }
    private APlayer[] Players { init; get; }
}
