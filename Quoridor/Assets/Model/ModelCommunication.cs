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

        private readonly CellCoordinates _whiteStartCoordinates = new CellCoordinates(8, 4);
        private readonly CellCoordinates _blackStartCoordinates = new CellCoordinates(0, 4);

        private readonly IView _view;

        private PlayerType _currentTurnPlayerType;
        private readonly Player _whitePlayer;
        private readonly Player _blackPlayer;

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

            _whitePlayer = new Player(_whiteStartCoordinates);
            _blackPlayer = new Player(_blackStartCoordinates);
        }

        #region Gamecycle logic

        public void StartNewGame(GameMode gameMode)
        {
            _gameMode = gameMode;
            _bot = new RandomBot(_blackStartCoordinates);

            MovePlayerToCell(PlayerType.Black, _blackStartCoordinates);
            MovePlayerToCell(PlayerType.White, _whiteStartCoordinates);

            _currentTurnPlayerType = PlayerType.White;
            ShowAvailableMovesForCurrentPawn();
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
                CellCoordinates currentPawnCoordinates = GetPlayerByPlayerType(_currentTurnPlayerType).CurrentCellCoordinates;
                if (!currentPawnCoordinates.Equals(cell))
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
            var cellCoordinatesArray = new List<CellCoordinates>();

            var lowerCell = new CellCoordinates(cellCoordinates.row + 1, cellCoordinates.column);
            var upperCell = new CellCoordinates(cellCoordinates.row - 1, cellCoordinates.column);
            var righterCell = new CellCoordinates(cellCoordinates.row, cellCoordinates.column + 1);
            var lefterCell = new CellCoordinates(cellCoordinates.row, cellCoordinates.column - 1);

            TryToAddCellToAvailableMoves(lowerCell, cellCoordinatesArray);
            TryToAddCellToAvailableMoves(upperCell, cellCoordinatesArray);
            TryToAddCellToAvailableMoves(righterCell, cellCoordinatesArray);
            TryToAddCellToAvailableMoves(lefterCell, cellCoordinatesArray);

            return cellCoordinatesArray;
        }

        private void ShowAvailableMovesForCurrentPawn()
        {
            Player currentTurnPlayer = GetPlayerByPlayerType(_currentTurnPlayerType);
            CellCoordinates currentCellPosition = currentTurnPlayer.CurrentCellCoordinates;

            IEnumerable<CellCoordinates> availableMoves = GetAvailableMovesFromCell(currentCellPosition);
            _view.HighlightCells(availableMoves);
        }

        #endregion

        #region Pawn logic

        private void MovePlayerToCell(PlayerType playerType, CellCoordinates cellCoordinates)
        {
            Player player = GetPlayerByPlayerType(playerType);
            CellCoordinates oldCellCoordinates = player.CurrentCellCoordinates;

            Cell oldCell = _cells[oldCellCoordinates.row, oldCellCoordinates.column];
            Cell newCell = _cells[cellCoordinates.row, cellCoordinates.column];

            oldCell.MakeFree();
            newCell.MakeBusy();

            player.MoveToCell(cellCoordinates);
            _view.MovePawnToCell(playerType, cellCoordinates);
        }

        public void MoveCurrentPlayerToCell(CellCoordinates cellCoordinates)
        {
            MovePlayerToCell(_currentTurnPlayerType, cellCoordinates);

            if (CheckCurrentPlayerVictory())
            {
                _view.UnhighlightAllCells();
                _view.ShowVictory(_currentTurnPlayerType);
            }
            else
            {
                if (_gameMode.Equals(GameMode.PlayerVsComputer))
                {
                    ChangeCurrentTurnPlayer();
                    switch (_bot.MakeMove(GetAvailableMovesFromCell(_blackPlayer.CurrentCellCoordinates)))
                    {
                        case MoveType.MoveToCell:
                            MovePlayerToCell(_currentTurnPlayerType, _bot.CellToMove);
                            break;
                        case MoveType.PlaceWall:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                
                ChangeCurrentTurnPlayer();
                ShowAvailableMovesForCurrentPawn();
            }
        }

        private void ChangeCurrentTurnPlayer()
        {
            _currentTurnPlayerType = _currentTurnPlayerType switch
            {
                PlayerType.White => PlayerType.Black,
                PlayerType.Black => PlayerType.White,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Player GetPlayerByPlayerType(PlayerType playerType)
        {
            return playerType switch
            {
                PlayerType.White => _whitePlayer,
                PlayerType.Black => _blackPlayer,
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };
        }

        private bool CheckCurrentPlayerVictory()
        {
            int currentPawnRow = GetPlayerByPlayerType(_currentTurnPlayerType).CurrentCellCoordinates.row;
            return _currentTurnPlayerType switch
            {
                PlayerType.White => currentPawnRow == _blackStartCoordinates.row,
                PlayerType.Black => currentPawnRow == _whiteStartCoordinates.row,
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
