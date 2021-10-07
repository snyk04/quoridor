using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Game;
using Quoridor.Model.Players;
using Quoridor.View;

namespace Quoridor.Model
{
    public class ModelCommunication : IModel
    {
        private readonly IView _view;

        internal PossibleMoves PossibleMoves { get; }
        internal CellsManager CellsManager { get; }
        internal GameCycle GameCycle { get; }
        internal PlayersController PlayersController { get; }
        
        public ModelCommunication(IView view)
        {
            _view = view;

            CellsManager = new CellsManager(this);
            GameCycle = new GameCycle(this);
            PlayersController = new PlayersController(
                this,
                new CellCoordinates(8, 4),
                new CellCoordinates(0, 4)
                );
            PossibleMoves = new PossibleMoves(this);
        }
        
        public void StartNewGame(GameMode gameMode)
        {
            GameCycle.StartNewGame(gameMode);
        }
        internal void EndGame(PlayerType winner)
        {
            _view.ShowVictory(winner);
        }
        public void MoveCurrentPlayerToCell(CellCoordinates cellCoordinates)
        {
            PlayersController.MoveCurrentPlayerToCell(cellCoordinates);
        }
        public void TryToPlaceWall(CellCoordinates wallCoordinates)
        {
            PlayersController.CurrentPlayerTryToPlaceWall(wallCoordinates);
        }

        internal void HighlightAvailableCells(IEnumerable<CellCoordinates> availableCells)
        {
            _view.HighlightCells(availableCells);
        }
        internal void MovePlayerToCell(PlayerType playerType, CellCoordinates cellCoordinates)
        {
            _view.MovePlayerToCell(playerType, cellCoordinates);
        }
        internal void PlaceWall(CellCoordinates wallCoordinates)
        {
            _view.PlaceWall(wallCoordinates);
        }
    }
}
