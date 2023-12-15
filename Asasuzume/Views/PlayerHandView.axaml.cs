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
            defaultBindingMode: BindingMode.TwoWay);

        public APlayer Player { get; set; }

        public PlayerHandView()
        {
            InitializeComponent();

            this.WhenActivated(_ =>
            {
                var p = GetValue(PlayerProperty);
                ;
            });
        }
    }
}
