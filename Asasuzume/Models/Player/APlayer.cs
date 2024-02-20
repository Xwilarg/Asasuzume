﻿using Asasuzume.Models.Services;
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

        /// <summary>
        /// Check if the player does one of the various combinaison
        /// </summary>
        /// <param name="combinaisons">For each combinaisons, associate each list of tile concerned</param>
        public abstract void CheckCombinations(Dictionary<Combination, List<MahjongTile[]>> combinaisons);

        public ObservableCollection<MahjongTile> Deck { set; get; } = [];
        public ObservableCollection<MahjongTile> Discarded { get; } = [];

        public MahjongTile? LastDiscarded => Discarded.Any() ? Discarded[^1] : null;

        public void RemoveLastDiscarded()
        {
            Discarded.Remove(LastDiscarded!);
        }

        /// <summary>
        /// What is our turn order
        /// </summary>
        public int Index { set; get; } = -1;
    }
}