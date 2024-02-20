﻿using Asasuzume.Models.Player;
using Asasuzume.Models.Tile;
using Avalonia.Metadata;
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
        private readonly List<APlayer> _pendingPlayers = new();

        /// <inheritdoc/>
        public void EndTurn()
        {
            // Check conditions for AIs
            List<(APlayer p, Combinaison[] comb)> pending = new();
            for (int i = 0; i < _players.Count; i++)
            {
                var p = _players[i];

                var combs = new List<Combinaison>();
                if (p.CanChii(LastThrownTile)) combs.Add(Combinaison.Chii);

                if (combs.Any())
                {
                    pending.Add((p, combs.ToArray()));
                }
            }

            if (pending.Any()) // We are waiting for players to confirm a combinaison
            {
                _pendingPlayers.AddRange(pending.Select(x => x.p));

                foreach (var p in pending)
                {
                    p.p.CheckCombinaison(p.comb);
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
                StartNextTurn();
            }
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
