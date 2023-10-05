using System;
using System.Collections.Generic;
using System.Linq;

namespace Asasuzume.Models
{
    public class MahjongDeck : IMahjongDeck
    {
        public MahjongDeck()
        {
            // Fill deck with all tiles
            var categories = new[] { "Bamboo", "Character", "Dot" };
            foreach (var c in categories)
            {
                for (int i = 1; i < 10; i++)
                {
                    if (i == 5 && useRedFives)
                    {
                        _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{c}5.svg"), 3));
                        _refDeck.Add(new($"{TilePath}{c}5Red.svg"));
                    }
                    else
                    {
                        _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{c}{i}.svg"), 4));
                    }
                }
            }
            var otherTiles = new[] { "East", "West", "North", "South", "Red", "White", "Green" };
            foreach (var t in otherTiles)
            {
                _refDeck.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{t}.svg"), 4));
            }

            _deck = new(_refDeck);
        }

        public MahjongTile DrawTile()
        {
            var nb = _rand.Next(_deck.Count);
            var tile = _deck[nb];
            _deck.RemoveAt(nb);
            return tile;
        }

        private string TilePath => "/Assets/Tiles/";

        private const bool useRedFives = true;

        private List<MahjongTile> _deck = new();
        private readonly List<MahjongTile> _refDeck = new();
        private Random _rand = new();
    }
}
