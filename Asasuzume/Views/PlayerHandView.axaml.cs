using Asasuzume.ViewModels;
using Avalonia;
using Avalonia.Data;
using Avalonia.ReactiveUI;

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
            set
            {
                SetValue(PlayerProperty, value);
                DataContext = new PlayerHandViewModel(value);
            }
        }

        public PlayerHandView()
        {
            InitializeComponent();
        }
    }
}
