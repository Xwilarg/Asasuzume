using System.Collections.Generic;

namespace Asasuzume.Models
{
    public class GameManager
    {
        public Player HumanPlayer => _players[0];
        private List<Player> _players = new()
        {
            new(), new(), new(), new() // 4P
        };
    }
}
