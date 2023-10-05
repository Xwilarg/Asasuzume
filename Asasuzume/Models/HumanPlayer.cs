using Asasuzume.Models.Services;
using ReactiveUI;
using Splat;

namespace Asasuzume.Models
{
    public class HumanPlayer : APlayer
    {
        public HumanPlayer()
            : base()
        {
            foreach (var card in Deck)
            {
                card.OnTileSelected = ReactiveCommand.Create(() =>
                {
                    Locator.Current.GetService<IGameManager>()!.EndTurn();
                });
            }
        }
    }
}
