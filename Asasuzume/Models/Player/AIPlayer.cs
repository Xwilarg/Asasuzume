using Asasuzume.Models.Services;
using Asasuzume.Models.Tile;
using Splat;
using System.Collections.Generic;

namespace Asasuzume.Models.Player
{
    public class AIPlayer : APlayer
    {
        public override void StartTurn()
        {
            base.StartTurn();
            Discard(Deck[0]);
            Locator.Current.GetService<IGameManager>()!.EndTurn();
        }

        public override void CheckCombinations(Dictionary<Combination, List<MahjongTile[]>> combinaisons)
        {
            Locator.Current.GetService<IGameManager>()!.DiscardPending(this);
        }
    }
}
