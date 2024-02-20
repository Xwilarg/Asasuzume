using Asasuzume.Models.Services;
using Asasuzume.Models.Tile;
using DynamicData;
using ReactiveUI;
using Splat;
using System.Linq;

namespace Asasuzume.Models.Player
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

        public override void CheckCombinaison(Combinaison[] combinaisons)
        {
            Locator.Current.GetService<IGameManager>()!.DiscardPending(this);
        }

        private void SortDeck()
        {
            var newDeck = Deck.OrderBy(x => (int)x.TileType * 10 + x.Value).ToArray();
            Deck.Clear();
            Deck.AddRange(newDeck);
        }
    }
}
