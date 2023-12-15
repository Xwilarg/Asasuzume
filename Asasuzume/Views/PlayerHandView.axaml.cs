using Asasuzume.Models;
using Asasuzume.ViewModels;
using Avalonia;
using Avalonia.Data;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Asasuzume.Views
{
    public partial class PlayerHandView : ReactiveUserControl<PlayerHandViewModel>
    {
        public static readonly StyledProperty<APlayer> PlayerProperty =
        AvaloniaProperty.Register<PlayerHandView, APlayer>("Player", defaultValue: null,
            defaultBindingMode: BindingMode.OneWay);

        public APlayer Player
        {
            get => GetValue(PlayerProperty);
        }

        public PlayerHandView()
        {
            InitializeComponent();

            this.WhenActivated(_ =>
            {
                ViewModel.Player = GetValue(PlayerProperty);
                ;
            });
        }
    }
}
