using Asasuzume.Models;
using DynamicData;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Asasuzume.ViewModels;

public class MainViewModel : ViewModelBase
{

    public MainViewModel()
    {
        for (int i = 0; i < 14; i++)
        {
            Items.Add(Locator.Current.GetService<IMahjongDeck>()!.DrawTile());
        }
    }

    private ObservableCollection<MahjongTile> Items { get; } = new();
}
