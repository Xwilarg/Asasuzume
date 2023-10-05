using System.Windows.Input;

namespace Asasuzume.Models
{
    public class MahjongTile
    {
        public MahjongTile(string imagePath, TileType tileType, int value)
        {
            ImagePath = imagePath;
            TileType = tileType;
            Value = value;
        }

        public override string ToString()
        {
            return $"{TileType}{Value}";
        }

        public string ImagePath { set; get; }
        public TileType TileType;
        public int Value;
        public ICommand OnTileSelected { set; get; }
    }
}
