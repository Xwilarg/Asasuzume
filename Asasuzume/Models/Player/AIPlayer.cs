using Asasuzume.Models.Services;
using Asasuzume.Models.Tile;
using Splat;

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

        public override void CheckCombinaison(Combinaison[] combinaisons)
        {
            Locator.Current.GetService<IGameManager>()!.DiscardPending(this);
        }
    }
}
