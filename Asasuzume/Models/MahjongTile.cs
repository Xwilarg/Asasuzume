using ReactiveUI;
using System.Windows.Input;

namespace Asasuzume.Models
{
    public class MahjongTile
    {
        public MahjongTile(string imagePath)
        {
            ImagePath = imagePath;
            OnTileSelected = ReactiveCommand.Create(() =>
            {

            });
        }

        public string ImagePath { set; get; }
        public ICommand OnTileSelected { set; get; }
    }
}
