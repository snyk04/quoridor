using System;
using System.Collections.Generic;
using Quoridor.Model.Players;
using Quoridor.View;

namespace Quoridor.Model
{
    public class ModelCommunication
    {
        #region Properties

        public const int AmountOfRows = 9;
        public const int AmountOfColumns = 9;

        private readonly CellCoordinates _firstPlayerStartCoordinates = new CellCoordinates(8, 4);
        private readonly CellCoordinates _secondPlayerStartCoordinates = new CellCoordinates(0, 4);

        private readonly IView _view;

        private PlayerType _currentTurnPlayerType;
        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;

        private readonly Cell[,] _cells;

        private GameMode _gameMode;
        private BaseBot _bot;

        #endregion

        public ModelCommunication(IView view)
        {
            _view = view;

            _cells = new Cell[AmountOfRows, AmountOfColumns];
            for (int i = 0; i < AmountOfRows; i++)
            {
                for (int j = 0; j < AmountOfColumns; j++)
                {
                    _cells[i, j] = new Cell();
                }
            }

            _firstPlayer = new Player(_firstPlayerStartCoordinates);
            _secondPlayer = new Player(_secondPlayerStartCoordinates);
        }

        #region Gamecycle logic

        public void StartNewGame(GameMode gameMode)
        {
            _gameMode = gameMode;
            _bot = new RandomBot(_secondPlayerStartCoordinates);

            MovePlayerToCell(PlayerType.First, _firstPlayerStartCoordinates);
            MovePlayerToCell(PlayerType.Second, _secondPlayerStartCoordinates);

            _currentTurnPlayerType = PlayerType.First;
            ShowAvailableMovesForCurrentPlayer();
        }

        #endregion

        #region Available moves logic

        private void TryToAddCellToAvailableMoves(CellCoordinates cell, List<CellCoordinates> availableMoves)
        {
            if (!CheckIfCellIsReal(cell))
            {
                return;
            }

            if (CheckIfCellIsBusy(cell))
            {
                CellCoordinates currentTurnPlayerCoordinates = GetPlayerByPlayerType(_currentTurnPlayerType).CurrentCellCoordinates;
                if (!currentTurnPlayerCoordinates.Equals(cell))
                {
                    availableMoves.AddRange(GetAvailableMovesFromCell(cell));
                }
            }
            else
            {
                availableMoves.Add(cell);
            }
        }
        private List<CellCoordinates> GetAvailableMovesFromCell(CellCoordinates cellCoordinates)
        {
            var availableMoves = new List<CellCoordinates>();
            var uncheckedMoves = new List<CellCoordinates>()
            {
                new CellCoordinates(cellCoordinates.row + 1, cellCoordinates.column),
                new CellCoordinates(cellCoordinates.row - 1, cellCoordinates.column),
                new CellCoordinates(cellCoordinates.row, cellCoordinates.column + 1),
                new CellCoordinates(cellCoordinates.row, cellCoordinates.column - 1)
            };

            foreach (CellCoordinates possibleMove in uncheckedMoves)
            {
                TryToAddCellToAvailableMoves(possibleMove, availableMoves);
            }

            return availableMoves;
        }
        private void ShowAvailableMovesForCurrentPlayer()
        {
            Player currentTurnPlayer = GetPlayerByPlayerType(_currentTurnPlayerType);
            CellCoordinates currentTurnPlayerCoordinates = currentTurnPlayer.CurrentCellCoordinates;

            IEnumerable<CellCoordinates> availableMoves = GetAvailableMovesFromCell(currentTurnPlayerCoordinates);
            _view.HighlightCells(availableMoves);
        }

        #endregion

        #region Player logic

        private void MovePlayerToCell(PlayerType playerType, CellCoordinates cellCoordinates)
        {
            Player player = GetPlayerByPlayerType(playerType);
            CellCoordinates oldCellCoordinates = player.CurrentCellCoordinates;

            Cell oldCell = _cells[oldCellCoordinates.row, oldCellCoordinates.column];
            Cell newCell = _cells[cellCoordinates.row, cellCoordinates.column];

            oldCell.IsBusy = false;
            newCell.IsBusy = true;

            player.MoveToCell(cellCoordinates);
            _view.MovePlayerToCell(playerType, cellCoordinates);
            
            if (CheckPlayerVictory(playerType))
            {
                _view.UnhighlightAllCells();
                _view.ShowVictory(_currentTurnPlayerType);
            }
        }
        public void MoveCurrentPlayerToCell(CellCoordinates cellCoordinates)
        {
            MovePlayerToCell(_currentTurnPlayerType, cellCoordinates);

            if (_gameMode.Equals(GameMode.PlayerVsComputer))
            {
                ChangeCurrentTurnPlayer();
                switch (_bot.MakeMove(GetAvailableMovesFromCell(_bot.CurrentCellCoordinates)))
                {
                    case MoveType.MoveToCell:
                        MovePlayerToCell(_currentTurnPlayerType, _bot.CellToMove);
                        break;
                    case MoveType.PlaceWall:
                        // PlaceWall(WallCoordinates _bot.WallCoordinates);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
                
            ChangeCurrentTurnPlayer();
            ShowAvailableMovesForCurrentPlayer();
        }
        
        private void ChangeCurrentTurnPlayer()
        {
            _currentTurnPlayerType = _currentTurnPlayerType switch
            {
                PlayerType.First => PlayerType.Second,
                PlayerType.Second => PlayerType.First,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Player GetPlayerByPlayerType(PlayerType playerType)
        {
            return playerType switch
            {
                PlayerType.First => _firstPlayer,
                PlayerType.Second => _gameMode.Equals(GameMode.PlayerVsComputer) ? _bot : _secondPlayer,
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };
        }

        private bool CheckPlayerVictory(PlayerType playerType)
        {
            int playerRow = GetPlayerByPlayerType(playerType).CurrentCellCoordinates.row;
            return playerType switch
            {
                PlayerType.First => playerRow == _secondPlayerStartCoordinates.row,
                PlayerType.Second => playerRow == _firstPlayerStartCoordinates.row,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        #endregion

        #region Cell logic

        private bool CheckIfCellIsReal(CellCoordinates cellCoordinates)
        {
            bool condition1 = cellCoordinates.row < AmountOfRows;
            bool condition2 = cellCoordinates.row >= 0;
            bool condition3 = cellCoordinates.column < AmountOfColumns;
            bool condition4 = cellCoordinates.column >= 0;

            return condition1 & condition2 & condition3 & condition4;
        }
        private bool CheckIfCellIsBusy(CellCoordinates cellCoordinates)
        {
            return _cells[cellCoordinates.row, cellCoordinates.column].IsBusy;
        }

        #endregion
    }
}
