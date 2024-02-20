using Asasuzume.Models.Player;
using Asasuzume.Models.Tile;
using Splat;
using System.Collections.Generic;
using System.Linq;

namespace Asasuzume.Models.Services
{
    public class GameManager : IGameManager
    {
        /// <summary>
        /// Players we are waiting for before starting next turn
        /// </summary>
        private readonly List<APlayer> _pendingPlayers = [];
        private readonly Dictionary<APlayer, (Combination Combination, MahjongTile[] Tiles)> _pendingCombinations = new();

        /// <inheritdoc/>
        public void EndTurn()
        {
            // Check conditions for AIs
            List<(APlayer p, Dictionary<Combination, List<MahjongTile[]>> comb)> pending = [];
            for (int i = 0; i < _players.Count; i++)
            {
                var p = _players[i];

                var combs = new Dictionary<Combination, List<MahjongTile[]>>();
                var chii = p.CanChii(LastThrownTile!);
                if (chii.Any()) combs.Add(Combination.Chii, chii);

                if (combs.Any())
                {
                    pending.Add((p, combs));
                }
            }

            if (pending.Any()) // We are waiting for players to confirm a combinaison
            {
                _pendingPlayers.AddRange(pending.Select(x => x.p));

                foreach (var p in pending)
                {
                    p.p.CheckCombinations(p.comb);
                }
            }
            else // No pending player so we directly start next turn
            {
                StartNextTurn();
            }
        }

        /// <inheritdoc/>
        public void DiscardPending(APlayer p)
        {
            _pendingPlayers.Remove(p);
            if (!_pendingPlayers.Any())
            {
                if (_pendingCombinations.Any())
                {
                    // If any player did a combination, we select the best one
                    var combs = _pendingCombinations.OrderByDescending(x => x.Value.Combination);
                    var best = combs.First();

                    var combTiles = best.Value.Tiles.ToList();

                    // One of the tiles isn't in the player hand but is the last discarded one
                    combTiles.Remove(LastThrownTile!);
                    _players[_turnIndex].RemoveLastDiscarded();

                    // Discard (for now) all tiles from the player hand related to the combination
                    foreach (var tile in combTiles)
                    {
                        best.Key.Discard(tile);
                    }

                    _pendingCombinations.Clear();
                    SkipToMyTurn(best.Key.Index);
                }
                else
                {
                    StartNextTurn();
                }
            }
        }

        /// <inheritdoc/>
        public void SelectCombination(APlayer p, Combination comb, MahjongTile[] tiles)
        {
            _pendingCombinations.Add(p, (comb, tiles));
            DiscardPending(p);
        }

        public void SkipToMyTurn(int index)
        {
            _turnIndex = index;
            _players[_turnIndex].StartTurn();
        }

        /// <inheritdoc/>
        public void StartNextTurn()
        {
            _turnIndex++;
            if (_turnIndex == _players.Count)
            {
                _turnIndex = 0;
            }

            _players[_turnIndex].StartTurn();
        }

        /// <inheritdoc/>
        public void RegisterPlayer(APlayer player)
        {
            player.Index = _players.Count;

            if (!_players.Any()) // First player get an additional tile for his first turn
            {
                player.AddTile(Locator.Current.GetService<IMahjongDeck>()!.DrawTile());
            }
            _players.Add(player);
        }

        /// <inheritdoc/>
        public APlayer GetPlayer(int index)
        {
            return _players[index];
        }

        public MahjongTile? LastThrownTile => _players[_turnIndex].LastDiscarded;

        public bool IsMyTurnCurrent(int me) => _turnIndex == me;
        public bool IsMyTurnNext(int me) => (me == 0 && _turnIndex == _players.Count - 1) || _turnIndex == me - 1;

        private readonly List<APlayer> _players = [];
        private int _turnIndex;
    }
}
