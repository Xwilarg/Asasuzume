using System.Windows.Input;

namespace Asasuzume.Models.Tile
{
    public class MahjongTile
    {
        public MahjongTile(string imagePath, TileType tileType, int value, bool isRedDora)
        {
            ImagePath = imagePath;
            TileType = tileType;
            Value = value;
            IsRedDora = isRedDora;
        }

        public override string ToString()
        {
            return $"{TileType}{Value}";
        }

        public string ImagePath { set; get; }
        public TileType TileType;
        public int Value;
        public bool IsRedDora;
        public ICommand OnTileSelected { set; get; }
    }
}
