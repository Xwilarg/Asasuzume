using Asasuzume.Models.Player;
using Asasuzume.Models.Tile;
using System.Collections.ObjectModel;

namespace Asasuzume.ViewModels;

public class PlayerHandViewModel : ViewModelBase
{
    public APlayer Player { set; get; }

    public ObservableCollection<MahjongTile> Discarded => Player?.Discarded;
}
