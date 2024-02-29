using Asasuzume.Models.Services;
using Splat;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using Asasuzume.Models.Tile;
using System.Collections.Generic;

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

        /// <summary>
        /// Remove a tile from the hand
        /// Compared to <seealso cref="Discard(MahjongTile)"/> this just get rid of the tile
        /// This is for example used when doing a combinations and the tiles are moved elsewhere
        /// </summary>
        public void RemoveFromHand(MahjongTile tile)
        {
            if (!Deck.Contains(tile))
            {
                throw new InvalidOperationException("Tile isn't contained in the deck");
            }
            Deck.Remove(tile);
        }

        /// <summary>
        /// Remove a tile from the hand and move to discard pile
        /// </summary>
        public void Discard(MahjongTile tile)
        {
            RemoveFromHand(tile);
            Discarded.Add(tile);
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

        /// <summary>
        /// Return all sequences of 3 in the code
        /// </summary>
        public List<MahjongTile[]> CanChii(MahjongTile tile)
        {
            List<MahjongTile[]> combinaisons = [];

            var tiles = Deck.Where(x => x.TileType == tile.TileType);
            foreach (var prev in tiles.Where(x => x.Value == tile.Value - 1)) // Start at previous tile...
            {
                List<MahjongTile> sequence = [tile, prev];
                foreach (var prev2 in tiles.Where(x => x.Value == tile.Value - 2)) // Add with tile - 2
                {
                    List<MahjongTile> res = new(sequence)
                    {
                        prev2
                    };
                    combinaisons.Add([.. res]);
                }
                foreach (var next in tiles.Where(x => x.Value == tile.Value + 1)) // Add with tile + 1
                {
                    List<MahjongTile> res = new(sequence)
                    {
                        next
                    };
                    combinaisons.Add([.. res]);
                }
            }
            foreach (var next in tiles.Where(x => x.Value == tile.Value + 1)) // Start at next tile...
            {
                List<MahjongTile> sequence = [tile, next];
                foreach (var next2 in tiles.Where(x => x.Value == tile.Value + 2)) // Add with tile + 2
                {
                    List<MahjongTile> res = new(sequence)
                    {
                        next2
                    };
                    combinaisons.Add([.. res]);
                }
            }
            return combinaisons;
        }

        public List<MahjongTile[]> CanPon(MahjongTile tile)
        {
            var matching = Deck.Where(x => x.Value == tile.Value && x.TileType == tile.TileType).ToArray();
            if (matching.Length >= 2)
            {
                List<MahjongTile[]> tiles = [];
                for (int i = 0; i < matching.Length - 1; i++)
                {
                    for (int y = i + 1; y < matching.Length - 1; y++)
                    {
                        tiles.Add([tile, Deck[i], Deck[y]]);
                    }
                }
                return tiles;
            }
            return [];
        }

        public List<MahjongTile[]> CanKan(MahjongTile tile)
        {
            var matching = Deck.Where(x => x.Value == tile.Value && x.TileType == tile.TileType).ToArray();
            if (matching.Length == 3) // We can't have more than 4 anyway
            {
                return [ [ ..matching, tile ] ];
            }
            return [];
        }

        public void RemoveLastDiscarded()
        {
            Discarded.Remove(LastDiscarded!);
        }

        public void AddCombination(MahjongTile[] tiles)
        {
            Combinations.Add(tiles);
        }

        /// <summary>
        /// Check if the player does one of the various combinaison
        /// </summary>
        /// <param name="combinaisons">For each combinaisons, associate each list of tile concerned</param>
        public abstract void CheckCombinations(Dictionary<Combination, List<MahjongTile[]>> combinaisons);

        public ObservableCollection<MahjongTile> Deck { set; get; } = [];
        public ObservableCollection<MahjongTile> Discarded { get; } = [];
        public ObservableCollection<MahjongTile[]> Combinations { get; } = [];

        public MahjongTile? LastDiscarded => Discarded.Any() ? Discarded[^1] : null;

        /// <summary>
        /// What is our turn order
        /// </summary>
        public int Index { set; get; } = -1;
    }
}
