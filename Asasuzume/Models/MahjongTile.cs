namespace Asasuzume.Models
{
    public class MahjongTile
    {
        public MahjongTile(string imagePath)
        {
            ImagePath = imagePath;
        }

        public string ImagePath { set; get; }
    }
}
