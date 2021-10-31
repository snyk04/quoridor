using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Quoridor.IO;
using Quoridor.Model;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.Controller
{
    public class GameManager
    {
        private readonly IModel _model;
        
        public Coordinates[] AvailableCells { get; set; }
        public Coordinates[] AvailableWalls { get; set; }
        
        private bool _isGameStarted;
        private bool _isGameStopped;

        public GameManager(IModel model)
        {
            _model = model;
            
            _isGameStarted = false;
            _isGameStopped = false;
        }

        public void StartGame()
        {
            GameLoop();
        }
        public void StopGame()
        {
            _isGameStopped = true;
        }
        
        private void GameLoop()
        {
            while (!_isGameStopped)
            {
                if (!CommandController.TryToReadCommand(out Command command, out string[] arguments))
                {
                    continue;
                }
                
                switch (command)
                {
                    case Command.Move or Command.Jump:
                        TryToMove(command, arguments);
                        continue;
                    case Command.Wall:
                        TryToPlace(command, arguments);
                        continue;
                    case Command.Black or Command.White:
                        TryToStartGame(command);
                        continue;
                    default:
                        continue;
                }
            }
        }

        private void TryToMove(Command command, IReadOnlyList<string> arguments)
        {
            if (!CommandController.CheckArgumentsAmount(command, arguments, 1))
            {
                return;
            }

            if (!CommandController.TryToConvert(CellsConverter.MixedToNumber, arguments, command, out Coordinates cellCoordinates))
            {
                return;
            }
                        
            if (!AvailableCells.Contains(cellCoordinates))
            {
                Console.WriteLine("you can't move here");
                return;
            }
            
            _model.MoveCurrentPlayerToCell(cellCoordinates);
        }
        private void TryToPlace(Command command, IReadOnlyList<string> arguments)
        {
            if (!CommandController.CheckArgumentsAmount(command, arguments, 1))
            {
                return;
            }
            
            if (!CommandController.TryToConvert(WallsConverter.MixedToNumber, arguments, command, out Coordinates wallCoordinates))
            {
                return;
            }
                        
            if (!AvailableWalls.Contains(wallCoordinates))
            {
                Console.WriteLine("you can't place wall here");
                return;
            }
            
            _model.PlaceCurrentPlayerWall(wallCoordinates);
        }
        private void TryToStartGame(Command command)
        {
            if (_isGameStarted)
            {
                Console.WriteLine("game is already started!");
            }

            PlayerType whitePlayer = command switch
            {
                Command.White => PlayerType.RandomBot,
                Command.Black => PlayerType.Player1,
                _ => throw new ArgumentOutOfRangeException()
            };
            PlayerType blackPlayer = command switch
            {
                Command.White => PlayerType.Player1,
                Command.Black => PlayerType.RandomBot,
                _ => throw new ArgumentOutOfRangeException()
            };

            _isGameStarted = true;
            _model.StartNewGame(whitePlayer, blackPlayer);
        }
    }
}
