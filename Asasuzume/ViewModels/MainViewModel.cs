using Asasuzume.Models;
using Asasuzume.Models.Services;
using ReactiveUI;
using Splat;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Asasuzume.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        Chii = ReactiveCommand.Create(() => { });
        Pon = ReactiveCommand.Create(() =>
        {
        });
        Kan = ReactiveCommand.Create(() => { });
        Ron = ReactiveCommand.Create(() => { });
        Draw = ReactiveCommand.Create(() => { });

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

    private ICommand Chii { get; }
    private ICommand Pon { get; }
    private ICommand Kan { get; }
    private ICommand Ron { get; }
    private ICommand Draw { get; }
}
