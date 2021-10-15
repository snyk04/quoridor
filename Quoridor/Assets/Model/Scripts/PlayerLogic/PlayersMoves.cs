using System;
using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public sealed class PlayersMoves
    {
        private readonly ModelCommunication _model;
        
        private const int DefaultAmountOfWalls = 10;

        private readonly Coordinates _firstPlayerStartPosition = new Coordinates(8, 4);
        private readonly Coordinates _secondPlayerStartPosition = new Coordinates(0, 4);
        
        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }

        public Player _currentPlayer;

        public PlayerType CurrentPlayerType => _currentPlayer.Type;
        public PlayerType CurrentPlayerOpponentType
        {
            get
            {
                return CurrentPlayerType switch
                {
                    PlayerType.First => PlayerType.Second,
                    PlayerType.Second => PlayerType.First,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        public PlayersMoves(ModelCommunication model)
        {
            _model = model;
            
            _model.GameCycle.GameStarted += StartGame;
            _model.GameCycle.GameStopped += NullifyMoveEndEvents;
        }

        public void MoveCurrentPlayerToCell(Coordinates cellCoordinates)
        {
            _currentPlayer.MakeMove(MoveType.Move, cellCoordinates);
        }
        public void PlaceCurrentPlayerWall(Coordinates wallCoordinates)
        {
            _currentPlayer.MakeMove(MoveType.PlaceWall, wallCoordinates);
        }

        private void ChangeCurrentPlayer()
        {
            _currentPlayer = _currentPlayer.Type switch
            {
                PlayerType.First => SecondPlayer,
                PlayerType.Second => FirstPlayer,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        private void SetCurrentPlayerAvailableMoves()
        {
            Coordinates[] cells = _model.PossibleMoves.AvailableCells(_currentPlayer.Position);
            Coordinates[] walls = _model.PossibleMoves.AvailableWalls();
            _currentPlayer.SetAvailableMoves(cells, walls);
        }

        private void InitializePlayers(GameMode gameMode)
        {
            FirstPlayer = gameMode switch
            {
                GameMode.PlayerVsPlayer => new Player(_model, PlayerType.First, _firstPlayerStartPosition, DefaultAmountOfWalls, _secondPlayerStartPosition.row),
                GameMode.PlayerVsComputer => new Player(_model, PlayerType.First, _firstPlayerStartPosition, DefaultAmountOfWalls, _secondPlayerStartPosition.row),
                GameMode.ComputerVsComputer => new RandomBot(_model, PlayerType.First, _firstPlayerStartPosition, DefaultAmountOfWalls, _secondPlayerStartPosition.row),
                _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
            SecondPlayer = gameMode switch
            {
                GameMode.PlayerVsPlayer => new Player(_model, PlayerType.Second, _secondPlayerStartPosition, DefaultAmountOfWalls, _firstPlayerStartPosition.row),
                GameMode.PlayerVsComputer => new RandomBot(_model, PlayerType.Second, _secondPlayerStartPosition, DefaultAmountOfWalls, _firstPlayerStartPosition.row),
                GameMode.ComputerVsComputer => new RandomBot(_model, PlayerType.Second, _secondPlayerStartPosition, DefaultAmountOfWalls, _firstPlayerStartPosition.row),
                _ => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
        }
        private void HandleMoveEndEvents()
        {
            FirstPlayer.MovePerformed += ChangeCurrentPlayer;
            FirstPlayer.MovePerformed += SetCurrentPlayerAvailableMoves;
            SecondPlayer.MovePerformed += ChangeCurrentPlayer;
            SecondPlayer.MovePerformed += SetCurrentPlayerAvailableMoves;
        }
        private void NullifyMoveEndEvents()
        {
            FirstPlayer.MovePerformed -= ChangeCurrentPlayer;
            FirstPlayer.MovePerformed -= SetCurrentPlayerAvailableMoves;
            SecondPlayer.MovePerformed -= ChangeCurrentPlayer;
            SecondPlayer.MovePerformed -= SetCurrentPlayerAvailableMoves;
        }

        private void StartGame(GameMode gameMode)
        {
            InitializePlayers(gameMode);
            HandleMoveEndEvents();
            
            _currentPlayer = FirstPlayer;
            SetCurrentPlayerAvailableMoves();
        }
    }
}
