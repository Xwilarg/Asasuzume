using Asasuzume.Models.Services;
using DynamicData;
using ReactiveUI;
using Splat;
using System.Linq;

namespace Asasuzume.Models
{
    public class HumanPlayer : APlayer
    {
        public HumanPlayer()
            : base()
        {
            SortDeck();
        }

        public override void AddTile(MahjongTile tile)
        {
            base.AddTile(tile);
            tile.OnTileSelected = ReactiveCommand.Create(() =>
            {
                Discard(tile);
                SortDeck();
                Locator.Current.GetService<IGameManager>()!.EndTurn();
            });
        }

        private void SortDeck()
        {
            var newDeck = Deck.OrderBy(x => ((int)x.TileType * 10) + x.Value).ToArray();
            Deck.Clear();
            Deck.AddRange(newDeck);
        }
    }
}
