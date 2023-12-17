using Asasuzume.Models;
using Asasuzume.Models.Services;
using Asasuzume.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;

namespace Asasuzume.Views
{
    public partial class PlayerHandView : ReactiveUserControl<PlayerHandViewModel>
    {
        public static readonly StyledProperty<int> PlayerProperty =
        AvaloniaProperty.Register<PlayerHandView, int>(nameof(Player), defaultValue: -1,
            defaultBindingMode: BindingMode.TwoWay);

        public int Player
        {
            get => GetValue(PlayerProperty);
            set => SetValue(PlayerProperty, value);
        }

        public PlayerHandView()
        {
            ViewModel = new();
            InitializeComponent();

            this.WhenActivated(_ =>
            {
                ViewModel.Player = Locator.Current.GetService<IGameManager>()!.GetPlayer(GetValue(PlayerProperty));
                this.Find<ItemsControl>("Discard")!.ItemsSource = ViewModel.Discarded; // I somehow can't manage to find how to bind that properly from the XAML
            });
        }
    }
}
