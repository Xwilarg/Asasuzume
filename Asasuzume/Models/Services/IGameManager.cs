namespace Asasuzume.Models.Services
{
    public interface IGameManager
    {
        public void EndTurn();
        public void RegisterPlayer(APlayer player);
        public APlayer GetPlayer(int index);
    }
}
