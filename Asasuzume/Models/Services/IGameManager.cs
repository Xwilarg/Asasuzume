using Asasuzume.Models.Player;

namespace Asasuzume.Models.Services
{
    public interface IGameManager
    {
        /// <summary>
        /// End the current turn
        /// </summary>
        public void EndTurn();
        /// <summary>
        /// End the current turn by putting the next turn position as the current index
        /// </summary>
        /// <param name="index"></param>
        public void SkipToMyTurn(int index);
        /// <summary>
        /// Register a new player to the game
        /// </summary>
        /// <param name="player"></param>
        public void RegisterPlayer(APlayer player);
        /// <summary>
        /// Get a player given an index
        /// </summary>
        public APlayer GetPlayer(int index);
        /// <summary>
        /// If we are currently waiting for a player to do an action, say that is turn ended without doing anything
        /// </summary>
        /// <param name="p"></param>
        public void DiscardPending(APlayer p);
    }
}
