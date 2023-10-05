using Asasuzume.Models.Services;
using Splat;

namespace Asasuzume.Models
{
    public class AIPlayer : APlayer
    {
        public override void StartTurn()
        {
            base.StartTurn();
            Discard(Deck[0]);
            Locator.Current.GetService<IGameManager>()!.EndTurn();
        }
    }
}
