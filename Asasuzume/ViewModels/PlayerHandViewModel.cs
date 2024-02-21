using Asasuzume.Models.Player;
using Asasuzume.Models.Services;
using Asasuzume.Models.Tile;
using Splat;
using System.Collections.ObjectModel;

namespace Asasuzume.ViewModels;

public class PlayerHandViewModel : ViewModelBase
{
    public PlayerHandViewModel(int id)
    {
        _player = Locator.Current.GetService<IGameManager>()!.GetPlayer(id);
    }

    private readonly APlayer _player;

    public ObservableCollection<MahjongTile> Discarded => _player.Discarded;
}
