using Asasuzume.Models;
using Asasuzume.ViewModels;
using Avalonia;
using Avalonia.Data;
using Avalonia.ReactiveUI;

namespace Asasuzume.Views
{
    public partial class PlayerHandView : ReactiveUserControl<PlayerHandViewModel>
    {
        public static readonly StyledProperty<APlayer> PlayerProperty =
        AvaloniaProperty.Register<PlayerHandView, APlayer>(nameof(Player), defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);

        public APlayer Player
        {
            set => ViewModel!.Player = value;
            get => ViewModel!.Player;
        }

        public PlayerHandView()
        {
            ViewModel = new();

            InitializeComponent();
        }
    }
}
