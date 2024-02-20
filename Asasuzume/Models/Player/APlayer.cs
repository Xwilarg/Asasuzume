using Asasuzume.Models.Services;
using Splat;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using Asasuzume.Models.Tile;

namespace Asasuzume.Models.Player
{
    public abstract class APlayer
    {
        public APlayer()
        {
            var deck = Locator.Current.GetService<IMahjongDeck>()!;
            for (int i = 0; i < 13; i++)
            {
                AddTile(deck.DrawTile());
            }
        }

        public void Discard(MahjongTile tile)
        {
            if (!Deck.Contains(tile))
            {
                throw new InvalidOperationException("Tile isn't contained in the deck");
            }
            Discarded.Add(tile);
            Deck.Remove(tile);
        }

        public virtual void StartTurn()
        {
            AddTile(Locator.Current.GetService<IMahjongDeck>()!.DrawTile());
        }

        /// <summary>
        /// Add a tile to the player hand
        /// </summary>
        public virtual void AddTile(MahjongTile tile)
        {
            Deck.Add(tile);
        }

        public bool CanChii(MahjongTile tile)
        {
            var tiles = Deck.Where(x => x.TileType == tile.TileType);
            if (tiles.Any(x => x.Value == tile.Value - 1) && (tiles.Any(x => x.Value == tile.Value - 2) || tiles.Any(x => x.Value == tile.Value + 1)))
            {
                return true;
            }
            if (tiles.Any(x => x.Value == tile.Value + 1) && tiles.Any(x => x.Value == tile.Value + 2))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the player does one of the various combinaison
        /// </summary>
        /// <param name="combinaisons"></param>
        /// <returns></returns>
        public abstract void CheckCombinaison(Combinaison[] combinaisons);

        public ObservableCollection<MahjongTile> Deck { set; get; } = new();
        public ObservableCollection<MahjongTile> Discarded { get; } = new();

        public MahjongTile? LastDiscarded => Discarded.Any() ? Discarded[^1] : null;
    }
}
