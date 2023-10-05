using Asasuzume.Models.Services;
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
            for (int i = 0; i < newDeck.Length; i++)
            {
                Deck.Move(Deck.IndexOf(newDeck[i]), i);
            }
        }
    }
}
