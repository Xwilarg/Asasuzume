using Asasuzume.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Asasuzume.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string TilePath => "/Assets/Tiles/";

    public MainViewModel()
    {
        // Fill deck with all tiles
        var categories = new[] { "Bamboo", "Character", "Dot" };
        foreach (var c in categories)
        {
            for (int i = 1; i < 10; i++)
            {
                if (i == 5 && useRedFives)
                {
                    _tiles.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{c}5.svg"), 3));
                    _tiles.Add(new($"{TilePath}{c}5Red.svg"));
                }
                else
                {
                    _tiles.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{c}{i}.svg"), 4));
                }
            }
        }
        var otherTiles = new[] { "East", "West", "North", "South", "Red", "White", "Green" };
        foreach (var t in otherTiles)
        {
            _tiles.AddRange(Enumerable.Repeat(new MahjongTile($"{TilePath}{t}.svg"), 4));
        }

        // Fill hand
        for (int i = 0; i < _tiles.Count; i++)
        {
           // var rand = 
        }
        Items.Add(new("/Assets/Bamboo5.svg"));
    }

    private ObservableCollection<MahjongTile> Items { get; } = new();

    private readonly List<MahjongTile> _tiles = new();

    private const bool useRedFives = true;
}
