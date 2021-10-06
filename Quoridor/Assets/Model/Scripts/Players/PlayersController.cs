using System;
using Quoridor.Model.Cells;
using Quoridor.Model.Game;

namespace Quoridor.Model.Players
{
    public class PlayersController
    {
        private readonly ModelCommunication _model;

        private readonly CellCoordinates _firstPlayerStartPosition;
        private readonly CellCoordinates _secondPlayerStartPosition;

        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;
        
        private readonly Bot _bot;

        private Player _currentPlayer;
        private PlayerType _currentPlayerType;
        
        public event Action<CellCoordinates> OnPlayerChange;
        public event Action OnPlayerTurnEnd;
        public event Action OnBotTurnEnd;
        
        public PlayersController(ModelCommunication model, CellCoordinates firstPlayerStartPosition,
            CellCoordinates secondPlayerStartPosition)
        {
            _model = model;

            _firstPlayerStartPosition = firstPlayerStartPosition;
            _secondPlayerStartPosition = secondPlayerStartPosition;

            _firstPlayer = new Player(_firstPlayerStartPosition);
            _secondPlayer = new Player(_secondPlayerStartPosition);
            _bot = new RandomBot(_secondPlayerStartPosition);

            _model.GameCycle.OnGameStart += PlacePlayersAtStartPositions;
            _model.GameCycle.OnGameStart += WaitForTheFirstPlayer;
            _model.GameCycle.OnGameStart += HandleTurnEndEvents;
            
            _model.GameCycle.OnGameEnd += NullifyTurnEndEvents;
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
        
        private void PlacePlayersAtStartPositions()
        {
            MovePlayerToCell(PlayerType.First, _firstPlayer, _firstPlayerStartPosition);
            MovePlayerToCell(PlayerType.Second, _secondPlayer, _secondPlayerStartPosition);
            _bot.MoveToCell(_secondPlayerStartPosition);
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
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }
        public void MoveCurrentPlayerToCell(CellCoordinates cellCoordinates)
        {
            MovePlayerToCell(_currentPlayerType, _currentPlayer, cellCoordinates);
            
            OnPlayerTurnEnd?.Invoke();
        }

        private void WaitForTheFirstPlayer()
        {
            _currentPlayer = _firstPlayer;
            _currentPlayerType = PlayerType.First;

            OnPlayerChange?.Invoke(_firstPlayer.CurrentCellCoordinates);
        }
        private void WaitForTheNextPlayer()
        {
            ChangeCurrentPlayer();
            OnPlayerChange?.Invoke(_currentPlayer.CurrentCellCoordinates);
        }

        private void ComputeBotMove()
        {
            // TODO : maybe shitcode?
            _model.PossibleMoves.SetCurrentTurnPlayerCoordinates(_bot.CurrentCellCoordinates);
            
            switch (_bot.MakeMove(
                _model.PossibleMoves.GetPossibleMovesFromCell(_bot.CurrentCellCoordinates)))
            {
                case MoveType.MoveToCell:
                    MovePlayerToCell(PlayerType.Second, _bot, _bot.CellToMove);
                    break;
                case MoveType.PlaceWall:
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
