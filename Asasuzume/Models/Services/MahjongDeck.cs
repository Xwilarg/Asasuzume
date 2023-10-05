using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asasuzume.Models.Services
{
    public class MahjongDeck : IMahjongDeck
    {
        public MahjongDeck()
        {
            // Fill deck with all tiles
            var categories = new[] { TileType.DOT, TileType.CHARACTER, TileType.BAMBOO };
            foreach (var c in categories)
            {
                var catStr = c.ToString()[0] + c.ToString()[1..].ToLowerInvariant();

                for (int i = 1; i < 10; i++)
                {
                    if (i == 5 && useRedFives)
                    {
                        _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{catStr}5.svg", c, 5), 3));
                        _refDeck.Add(new($"{TilePath}{catStr}5Red.svg", c, 5));
                    }
                    else
                    {
                        _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{catStr}{i}.svg", c, i), 4));
                    }
                }
            }
            var winds = new[] { "East", "West", "North", "South" };
            foreach (var w in winds)
            {
                _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{w}.svg", TileType.WIND, 0), 4)); // TODO: Right value
            }
            var dragons = new[] { "Green", "White", "Red" };
            foreach (var d in dragons)
            {
                _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{d}.svg", TileType.DRAGON, 0), 4)); // TODO: Right value
            }

            _deck = new(_refDeck);
        }

        public MahjongTile DrawTile()
        {
            var nb = _rand.Next(_deck.Count);
            var tile = _deck[nb];

            if (!Design.IsDesignMode) // Preview XAML designer to run out of tiles
            {
                _deck.RemoveAt(nb);
            }
            return tile;
        }

        private string TilePath => "/Assets/Tiles/";

        private const bool useRedFives = true;

        private List<MahjongTile> _deck = new();
        private readonly List<MahjongTile> _refDeck = new();
        private Random _rand = new();
    }
}
