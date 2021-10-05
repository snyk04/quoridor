using System;
using Quoridor.Model.Players;
using UnityEngine;

namespace Quoridor.Model.New
{
    public class PlayersController
    {
        private readonly NewModel _model;

        private readonly CellCoordinates _firstPlayerStartPosition;
        private readonly CellCoordinates _secondPlayerStartPosition;

        private Player _firstPlayer;
        private Player _secondPlayer;

        private Player _currentPlayer;
        private PlayerType _currentPlayerType;

        private RandomBot _randomBot;

        public event Action<CellCoordinates> OnPlayerChange;
        public event Action OnTurnEnd;

        public PlayersController(NewModel model, CellCoordinates firstPlayerStartPosition,
            CellCoordinates secondPlayerStartPosition)
        {
            _model = model;

            _firstPlayerStartPosition = firstPlayerStartPosition;
            _secondPlayerStartPosition = secondPlayerStartPosition;

            InitializePlayers();

            _randomBot = new RandomBot(_secondPlayerStartPosition);

            _model.GameCycle.OnGameStart += PlacePlayersAtStartPositions;
            _model.GameCycle.OnGameStart += () => WaitForThePlayer(_firstPlayer, PlayerType.First);
            _model.GameCycle.OnGameStart += DecideWhatToDoAboutBot;
            
            _model.GameCycle.OnGameEnd += UnsubscribeFromEvents;
        }

        private void DecideWhatToDoAboutBot()
        {
            UnsubscribeFromEvents();
            
            OnTurnEnd += _model.GameCycle.GameMode switch
            {
                GameMode.PlayerVsPlayer => WaitForTheNextPlayer,
                GameMode.PlayerVsComputer => ComputeBotMove,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        private void UnsubscribeFromEvents()
        {
            Debug.Log("GAME ENDED PT2");
            OnTurnEnd = null;
        }

        private void InitializePlayers()
        {
            _firstPlayer = new Player(_firstPlayerStartPosition);
            _secondPlayer = new Player(_secondPlayerStartPosition);
        }
        private void PlacePlayersAtStartPositions()
        {
            MovePlayerToCell(PlayerType.First, _firstPlayer, _firstPlayerStartPosition);
            MovePlayerToCell(PlayerType.Second, _secondPlayer, _secondPlayerStartPosition);
        }

        private void MovePlayerToCell(PlayerType playerType, Player player, CellCoordinates cellCoordinates)
        {
            CellCoordinates oldCellCoordinates = player.CurrentCellCoordinates;

            Cell oldCell = _model.CellsManager.Cells[oldCellCoordinates.row, oldCellCoordinates.column];
            Cell newCell = _model.CellsManager.Cells[cellCoordinates.row, cellCoordinates.column];

            oldCell.IsBusy = false;
            newCell.IsBusy = true;

            player.MoveToCell(cellCoordinates);
            _model.MovePlayerToCell(playerType, cellCoordinates);

            // TODO : hardcode
            switch (playerType)
            {
                case PlayerType.First:
                    if (cellCoordinates.row == _secondPlayerStartPosition.row)
                    {
                        _model.GameCycle.EndGame(playerType);
                    }
                    break;
                case PlayerType.Second:
                    if (cellCoordinates.row == _firstPlayerStartPosition.row)
                    {
                        _model.GameCycle.EndGame(playerType);
                        Debug.Log("GAME ENDED PT1");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }
        public void MoveCurrentPlayerToCell(CellCoordinates cellCoordinates)
        {
            MovePlayerToCell(_currentPlayerType, _currentPlayer, cellCoordinates);
            
            OnTurnEnd?.Invoke();
        }

        private void WaitForThePlayer(Player player, PlayerType playerType)
        {
            _currentPlayer = player;
            _currentPlayerType = playerType;

            OnPlayerChange?.Invoke(player.CurrentCellCoordinates);
        }
        private void WaitForTheNextPlayer()
        {
            ChangeCurrentPlayer();
            OnPlayerChange?.Invoke(_currentPlayer.CurrentCellCoordinates);
        }
        private void ComputeBotMove()
        {
            ChangeCurrentPlayer();
            
            // TODO : maybe shitcode?
            _model.PossibleMoves.SetCurrentTurnPlayerCoordinates(_randomBot.CurrentCellCoordinates);
            switch (_randomBot.MakeMove(
                _model.PossibleMoves.GetPossibleMovesFromCell(_randomBot.CurrentCellCoordinates)))
            {
                case MoveType.MoveToCell:
                    MovePlayerToCell(PlayerType.Second, _randomBot, _randomBot.CellToMove);
                    break;
                case MoveType.PlaceWall:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            WaitForTheNextPlayer();
        }
        
        private void ChangeCurrentPlayer()
        {
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
