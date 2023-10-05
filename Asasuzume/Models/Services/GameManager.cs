using Splat;
using System.Collections.Generic;
using System.Linq;

namespace Asasuzume.Models.Services
{
    public class GameManager : IGameManager
    {
        public void EndTurn()
        {
            _turnIndex++;
            if (_turnIndex == _players.Count)
            {
                _turnIndex = 0;
            }

            _players[_turnIndex].StartTurn();
        }

        public void RegisterPlayer(APlayer player)
        {
            if (!_players.Any()) // First player get an additional tile for his first turn
            {
                player.AddTile(Locator.Current.GetService<IMahjongDeck>()!.DrawTile());
            }
            _players.Add(player);
        }

        private readonly List<APlayer> _players = new();
        private int _turnIndex;
    }
}
