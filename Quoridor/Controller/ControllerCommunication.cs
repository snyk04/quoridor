using System;
using System.Collections.Generic;
using System.Linq;
using Quoridor.Model;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;
using Quoridor.View;

namespace Quoridor.Controller
{
    public class ControllerCommunication : IController
    {
        private readonly IView _view;
        private readonly IModel _model;

        private bool _isGameStarted;
        
        public Coordinates[] AvailableMoves { private get; set; }
        public Coordinates[] AvailableWalls { private get; set; }

        public ControllerCommunication()
        {
            _view = new ViewCommunication(this);
            _model = new ModelCommunication(_view);

            _isGameStarted = false;
        }
        
        public void StartGame()
        {
            GameLoop();
        }
        private void GameLoop()
        {
            while (true)
            {
                if (!TryToReadCommand(out Command command, out string[] arguments))
                {
                    continue;
                }
                
                switch (command)
                {
                    case Command.Move or Command.Jump:
                        if (!CheckArgumentsAmount(command, arguments, 1))
                        {
                            continue;
                        }

                        Coordinates cellCoordinates;
                        try
                        {
                            cellCoordinates = CellsConverter.MixedToNumber(arguments[0]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{command.ToString().ToLower()}: wrong argument");
                            continue;
                        }
                        
                        if (!AvailableMoves.Contains(cellCoordinates))
                        {
                            Console.WriteLine("you can't move here");
                            continue;
                        }
                        _model.MoveCurrentPlayerToCell(cellCoordinates);
                        continue;
                    case Command.Place:
                        if (!CheckArgumentsAmount(command, arguments, 1))
                        {
                            continue;
                        }
                        
                        Coordinates wallCoordinates;
                        try
                        {
                            wallCoordinates = WallsConverter.MixedToNumber(arguments[0]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{command.ToString().ToLower()}: wrong argument");
                            continue;
                        }
                        
                        if (!AvailableWalls.Contains(wallCoordinates))
                        {
                            Console.WriteLine("you can't place wall here");
                            continue;
                        }
                        _model.PlaceCurrentPlayerWall(wallCoordinates);
                        continue;
                    case Command.Black:
                        if (_isGameStarted)
                        {
                            Console.WriteLine("game is already started!");
                        }

                        _isGameStarted = true;
                        _model.StartNewGame(GameMode.PlayerVsComputer);
                        continue;
                    case Command.White:
                        if (_isGameStarted)
                        {
                            Console.WriteLine("game is already started!");
                        }

                        _isGameStarted = true;
                        _model.StartNewGame(GameMode.PlayerVsComputer);
                        continue;
                    default:
                        continue;
                }
            }
        }
        
        private static bool TryToReadCommand(out Command command, out string[] arguments)
        {
            command = default;
            arguments = default;
            
            string playerInput = CustomConsole.ReadLine();
            if (playerInput == null)
            {
                return false;
            }
            
            List<string> splittedPlayerInput = new List<string>(playerInput.Split(' '));
            string commandString = splittedPlayerInput[0];
            
            if (!Enum.TryParse(commandString, true, out command))
            {
                Console.WriteLine($"{commandString}: unknown command");
                return false;
            }

            splittedPlayerInput.Remove(commandString);
            arguments = splittedPlayerInput.ToArray();
            
            return true;
        }
        private bool CheckArgumentsAmount(Command command, IReadOnlyCollection<string> arguments, int goalAmount)
        {
            if (arguments == null)
            {
                return false;
            }
            if (arguments.Count == goalAmount)
            {
                return true;
            }
            
            Console.WriteLine($"{command.ToString().ToLower()}: wrong amount of arguments");
            return false;
        }
    }
}
