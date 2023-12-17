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

        public APlayer GetPlayer(int index)
        {
            return _players[index];
        }

        public MahjongTile? LastThrownTile => _players[_turnIndex].LastDiscarded;

        public bool IsMyTurnCurrent(int me) => _turnIndex == me;
        public bool IsMyTurnNext(int me) => (me == 0 && _turnIndex == _players.Count - 1) || _turnIndex == me - 1;

        private readonly List<APlayer> _players = new();
        private int _turnIndex;
    }
}
