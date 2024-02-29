using Asasuzume.Models.Tile;
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
            var categories = new[] { TileType.Dot, TileType.Character, TileType.Bamboo };
            foreach (var c in categories)
            {
                for (int i = 1; i < 10; i++)
                {
                    if (i == 5 && useRedFives)
                    {
                        _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{c}5.svg", c, value: 5, isRedDora: false), 3));
                        _refDeck.Add(new($"{TilePath}{c}5Red.svg", c, value: 5, isRedDora: true));
                    }
                    else
                    {
                        _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{c}{i}.svg", c, value: i, isRedDora: false), 4));
                    }
                }
            }
            var winds = new[] { "East", "West", "North", "South" };
            for (int i = 0; i < winds.Length; i++)
            {
                _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{winds[i]}.svg", TileType.Wind, value: i, isRedDora: false), 4));
            }
            var dragons = new[] { "Green", "White", "Red" };
            for (int i = 0; i < dragons.Length; i++)
            {
                _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{dragons[i]}.svg", TileType.Dragon, value: i, isRedDora: false), 4));
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
