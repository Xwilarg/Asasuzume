namespace Asasuzume.Models.Services
{
    public interface IGameManager
    {
        public void NextTurn();
        public void RegisterPlayer(Player player);
    }
}
