using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Common;
using Quoridor.Model.Pathfinding;
using Quoridor.Model.PlayerLogic;
using Quoridor.Model.Walls;
using Quoridor.View;

namespace Quoridor.Model
{
    public sealed class ModelCommunication : IModel
    {
        private readonly IView _view;
        
        public FieldPathFinder FieldPathFinder { get; }
        
        public GameCycle GameCycle { get; }
        public PlayerController PlayerController { get; }
        
        public CellsManager CellsManager { get; }
        public WallsManager WallsManager { get; }
        
        public PlayerMover PlayerMover { get; }
        
        public PossibleMoves PossibleMoves { get; }

        public ModelCommunication(IView view)
        {
            _view = view;

            FieldPathFinder = new FieldPathFinder(this);
            
            GameCycle = new GameCycle(this);
            PlayerController = new PlayerController(this);

            CellsManager = new CellsManager(this);
            WallsManager = new WallsManager();

            PlayerMover = new PlayerMover(this);
            
            PossibleMoves = new PossibleMoves(this);
        }

        public void StartNewGame(PlayerType whitePlayer, PlayerType blackPlayer)
        {
            GameCycle.StartNewGame(whitePlayer, blackPlayer);
        }
        public void StopGame(GameStopType gameStopType)
        {
            PlayerColor winner = PlayerController.GetWinner(gameStopType);
            _view.EndGame(winner);
        }

        public void MoveCurrentPlayerToCell(Coordinates cell)
        {
            PlayerController.MoveCurrentPlayerToCell(cell);
        }
        public void JumpCurrentPlayerToCell(Coordinates cell)
        {
            PlayerController.JumpCurrentPlayerToCell(cell);
        }
        public void PlaceCurrentPlayerWall(Coordinates wall)
        {
            PlayerController.PlaceCurrentPlayerWall(wall);
        }

        internal void MovePlayer(Player player, Coordinates coordinates)
        {
            _view.MovePlayerToCell(player, coordinates);
            PlayerMover.Move(player, coordinates);
        }
        internal void JumpPlayer(Player player, Coordinates coordinates)
        {
            _view.JumpPlayerToCell(player, coordinates);
            PlayerMover.Jump(player, coordinates);
        }
        internal void PlaceWall(Player player, Coordinates coordinates)
        {
            WallsManager.PlaceWall(player, coordinates);
            _view.PlaceWall(player, coordinates);
        }

        internal void ShowAvailableMoves(IEnumerable<Coordinates> moves)
        {
            _view.ShowAvailableMoves(moves);
        }
        internal void ShowAvailableJumps(IEnumerable<Coordinates> jumps)
        {
            _view.ShowAvailableJumps(jumps);
        }
        internal void ShowAvailableWalls(IEnumerable<Coordinates> walls)
        {
            _view.ShowAvailableWalls(walls);
        }
    }
}
