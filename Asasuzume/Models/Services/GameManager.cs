using System.Collections.Generic;

namespace Asasuzume.Models.Services
{
    public class GameManager : IGameManager
    {
        public void NextTurn()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterPlayer(Player player)
        {
            _players.Add(player);
        }

        private readonly List<Player> _players = new();
    }
}
