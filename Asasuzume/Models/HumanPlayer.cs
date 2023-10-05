using Asasuzume.Models.Services;
using ReactiveUI;
using Splat;

namespace Asasuzume.Models
{
    public class HumanPlayer : APlayer
    {
        public override void AddTile(MahjongTile tile)
        {
            base.AddTile(tile);
            tile.OnTileSelected = ReactiveCommand.Create(() =>
            {
                Discard(tile);
                Locator.Current.GetService<IGameManager>()!.EndTurn();
            });
        }
    }
}
