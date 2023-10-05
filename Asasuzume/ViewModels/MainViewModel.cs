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
    { }

    private ObservableCollection<MahjongTile> Items => _gm.HumanPlayer.Deck;

    private GameManager _gm = new();
}
