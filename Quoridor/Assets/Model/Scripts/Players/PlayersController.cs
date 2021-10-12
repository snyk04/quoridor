using System;
using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Game;
using UnityEngine;

namespace Quoridor.Model.Players
{
    public class PlayersController
    {
        private const int StartAmountOfWalls = 10;
        
        private readonly ModelCommunication _model;

        private readonly Coordinates _firstPlayerStartPosition;
        private readonly Coordinates _secondPlayerStartPosition;

        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;
        
        private readonly Bot _bot;

        private Player _currentPlayer;
        private PlayerType _currentPlayerType;
        
        // TODO : move it to GameCycle.cs!!!
        public event Action<Coordinates> OnPlayerChange;
        public event Action OnPlayerTurnEnd;
        public event Action OnBotTurnEnd;
        
        public PlayersController(ModelCommunication model, Coordinates firstPlayerStartPosition, Coordinates secondPlayerStartPosition)
        {
            _model = model;

            _firstPlayerStartPosition = firstPlayerStartPosition;
            _secondPlayerStartPosition = secondPlayerStartPosition;

            _firstPlayer = new Player(StartAmountOfWalls, _firstPlayerStartPosition);
            _secondPlayer = new Player(StartAmountOfWalls, _secondPlayerStartPosition);
            _bot = new RandomBot(StartAmountOfWalls, _secondPlayerStartPosition);

            _model.GameCycle.OnGameStart += HandleTurnEndEvents;
            _model.GameCycle.OnGameStart += ResetPlayersPositions;
            _model.GameCycle.OnGameStart += ResetPlayersAmountOfWalls;
            _model.GameCycle.OnGameStart += WaitForTheFirstPlayer;
            
            _model.GameCycle.OnGameEnd += NullifyTurnEndEvents;
        }
        
        public void MoveCurrentPlayerToCell(Coordinates cellCoordinates)
        {
            MovePlayerToCell(_currentPlayerType, _currentPlayer, cellCoordinates);
            
            OnPlayerTurnEnd?.Invoke();
        }
        public void CurrentPlayerTryToPlaceWall(Coordinates wallCoordinates)
        {
            TryToPlaceWall(_currentPlayer, wallCoordinates);
        }

        private void HandleTurnEndEvents()
        {
            NullifyTurnEndEvents();
            
            OnPlayerTurnEnd += _model.GameCycle.GameMode switch
            {
                GameMode.PlayerVsPlayer => WaitForTheNextPlayer,
                GameMode.PlayerVsComputer => BotTurn,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            OnBotTurnEnd += WaitForTheNextPlayer;
        }
        private void NullifyTurnEndEvents()
        {
            OnPlayerTurnEnd = null;
            OnBotTurnEnd = null;
        }
        
        private void ResetPlayersPositions()
        {
            MovePlayerToCell(PlayerType.First, _firstPlayer, _firstPlayerStartPosition);
            MovePlayerToCell(PlayerType.Second, _secondPlayer, _secondPlayerStartPosition);
            
            _bot.MoveToCell(_secondPlayerStartPosition);
        }
        private void ResetPlayersAmountOfWalls()
        {
            _firstPlayer.ResetAmountOfWalls();
            _secondPlayer.ResetAmountOfWalls();
            
            _bot.ResetAmountOfWalls();
        }

        private void MovePlayerToCell(PlayerType playerType, Player player, Coordinates cellCoordinates)
        {
            Coordinates oldCellCoordinates = player.CurrentCell;

            Cell oldCell = _model.CellsManager.GetCell(oldCellCoordinates);
            Cell newCell = _model.CellsManager.GetCell(cellCoordinates);

            oldCell.BecomeFree();
            newCell.BecomeBusy();

            player.MoveToCell(cellCoordinates);
            _model.MovePlayerToCell(playerType, cellCoordinates);
            
            CheckPlayerVictory(playerType, player);
        }
        private void TryToPlaceWall(Player player, Coordinates wallCoordinates)
        {
            if (player.AmountOfWalls < 1 || !_model.WallsManager.WallCanBePlaced(wallCoordinates))
            {
                return;
            }
            
            _model.WallsManager.PlaceWall(wallCoordinates, out List<Coordinates> overlappedWalls);

            if (!_model.PossibleMoves.IsAbilityToWin(_firstPlayer.CurrentCell, _secondPlayerStartPosition.row)
                || !_model.PossibleMoves.IsAbilityToWin(_secondPlayer.CurrentCell, _firstPlayerStartPosition.row))
            {
                _model.WallsManager.DestroyWall(wallCoordinates);
                return;
            }

            player.PlaceWall();
            _model.PlaceWall(wallCoordinates, overlappedWalls, _currentPlayerType, player.AmountOfWalls);

            if (!player.Equals(_bot))
            {
                OnPlayerTurnEnd?.Invoke();
            }
        }

        private void CheckPlayerVictory(PlayerType playerType, Player player)
        {
            var rowToWin = playerType switch
            {
                PlayerType.First => _secondPlayerStartPosition.row,
                PlayerType.Second => _firstPlayerStartPosition.row,
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };
            if (player.CurrentCell.row == rowToWin)
            {
                _model.GameCycle.EndGame(playerType);
            }
        }
        
        private void WaitForTheFirstPlayer()
        {
            _currentPlayer = _firstPlayer;
            _currentPlayerType = PlayerType.First;

            OnPlayerChange?.Invoke(_firstPlayer.CurrentCell);
        }
        private void WaitForTheNextPlayer()
        {
            ChangeCurrentPlayer();
            OnPlayerChange?.Invoke(_currentPlayer.CurrentCell);
        }

        private void ComputeBotMove()
        {
            _model.PossibleMoves.SetCurrentTurnPlayerCoordinates(_bot.CurrentCell);
            
            switch (_bot.MakeMove(
                _model.PossibleMoves.GetPossibleMovesFromCell(_bot.CurrentCell),
                _model.WallsManager.WallsThatCanBePlaced))
            {
                case MoveType.MoveToCell:
                    MovePlayerToCell(PlayerType.Second, _bot, _bot.CellToMove);
                    break;
                case MoveType.PlaceWall:
                    TryToPlaceWall(_bot, _bot.WallToPlace);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void BotTurn()
        {
            ChangeCurrentPlayer();
            
            ComputeBotMove();

            OnBotTurnEnd?.Invoke();
        }
        
        private void ChangeCurrentPlayer()
        {
            if (_model.GameCycle.GameMode == GameMode.PlayerVsComputer && _currentPlayerType == PlayerType.First)
            {
                _currentPlayerType = PlayerType.Second;
                _currentPlayer = _bot;
            }
            
            _currentPlayerType = _currentPlayerType switch
            {
                PlayerType.First => PlayerType.Second,
                PlayerType.Second => PlayerType.First,
                _ => throw new ArgumentOutOfRangeException()
            };

            _currentPlayer = _currentPlayerType switch
            {
                PlayerType.First => _firstPlayer,
                PlayerType.Second => _secondPlayer,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
