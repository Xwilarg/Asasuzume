using Asasuzume.Models.Services;
using Splat;
using System.Collections.ObjectModel;
using System;

namespace Asasuzume.Models
{
    public abstract class APlayer
    {
        public APlayer()
        {
            var deck = Locator.Current.GetService<IMahjongDeck>()!;
            for (int i = 0; i < 13; i++)
            {
                Deck.Add(deck.DrawTile());
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

        public ObservableCollection<MahjongTile> Deck { get; } = new();
        public ObservableCollection<MahjongTile> Discarded { get; } = new();
    }
}
