using System.Collections.Generic;

namespace Asasuzume.Models.Services
{
    public class GameManager : IGameManager
    {
        public void EndTurn()
        {
        }

        public void RegisterPlayer(APlayer player)
        {
            _players.Add(player);
        }

        private readonly List<APlayer> _players = new();
    }
}
