using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Game;
using Quoridor.Model.Players;
using Quoridor.Model.Walls;
using Quoridor.View;

namespace Quoridor.Model
{
    public class ModelCommunication : IModel
    {
        private readonly Coordinates _firstPlayerStartPosition = new Coordinates(8, 4);
        private readonly Coordinates _secondPlayerStartPosition = new Coordinates(0, 4);
        
        private readonly IView _view;

        internal CellsManager CellsManager { get; }
        internal GameCycle GameCycle { get; }
        internal PlayersController PlayersController { get; }
        internal PossibleMoves PossibleMoves { get; }
        internal WallsManager WallsManager { get; }
        
        public ModelCommunication(IView view)
        {
            _view = view;

            CellsManager = new CellsManager(this);
            GameCycle = new GameCycle(this);
            PlayersController = new PlayersController(
                this,
                _firstPlayerStartPosition,
                _secondPlayerStartPosition
                );
            PossibleMoves = new PossibleMoves(this);
            WallsManager = new WallsManager();
        }
        
        public void StartNewGame(GameMode gameMode)
        {
            GameCycle.StartNewGame(gameMode);
        }
        
        public void MoveCurrentPlayerToCell(Coordinates cellCoordinates)
        {
            PlayersController.MoveCurrentPlayerToCell(cellCoordinates);
        }
        public void TryToPlaceWall(Coordinates wallCoordinates)
        {
            PlayersController.CurrentPlayerTryToPlaceWall(wallCoordinates);
        }

        internal void EndGame(PlayerType winner)
        {
            _view.EndGame(winner);
        }
        internal void HighlightCells(IEnumerable<Coordinates> cells)
        {
            _view.HighlightCells(cells);
        }
        internal void MovePlayerToCell(PlayerType playerType, Coordinates cellCoordinates)
        {
            _view.MovePlayerToCell(playerType, cellCoordinates);
        }
        internal void PlaceWall(Coordinates wallCoordinates, IEnumerable<Coordinates> overlappedWalls)
        {
            _view.PlaceWall(wallCoordinates, overlappedWalls);
        }
    }
}
