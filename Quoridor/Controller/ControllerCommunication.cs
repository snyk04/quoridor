using System;
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
        
        public Coordinates[] AvailableMoves { private get; set; }
        public Coordinates[] AvailableWalls { private get; set; }

        public ControllerCommunication()
        {
            _view = new ViewCommunication(this);
            _model = new ModelCommunication(_view);
        }
        
        public void StartGame()
        {
            _model.StartNewGame(GameMode.PlayerVsComputer);
            
            GameLoop();
        }
        private void GameLoop()
        {
            while (true)
            {
                if (!TryToReadCommand(out Command command, out string arguments))
                {
                    Console.WriteLine("error!");
                }
                
                // TODO : refactor
                switch (command)
                {
                    case Command.Move or Command.Jump:
                        Coordinates cellCoordinates = CellsConverter.MixedToNumber(arguments);
                        if (!AvailableMoves.Contains(cellCoordinates))
                        {
                            Console.WriteLine("<- you can't move here");
                            break;
                        }
                        _model.MoveCurrentPlayerToCell(cellCoordinates);
                        break;
                    case Command.Place:
                        Coordinates wallCoordinates = WallsConverter.MixedToNumber(arguments);
                        if (!AvailableWalls.Contains(wallCoordinates))
                        {
                            Console.WriteLine("<- you can't place wall here");
                            break;
                        }
                        _model.PlaceCurrentPlayerWall(wallCoordinates);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        private static bool TryToReadCommand(out Command command, out string arguments)
        {
            command = default;
            arguments = default;
            
            Console.Write("-> ");

            string playerInput = Console.ReadLine();
            if (playerInput == null)
            {
                return false;
            }
            
            string[] splittedPlayerInput = playerInput.Split(' ');
            string commandString = splittedPlayerInput[0];
            
            if (!Enum.TryParse(commandString, true, out command))
            {
                Console.WriteLine($"{commandString}: unknown command");
                return false;
            }
            
            if (splittedPlayerInput.Length > 2)
            {
                Console.WriteLine($"{commandString}: too much arguments");
                return false;
            }
            
            arguments = splittedPlayerInput[1];    
            
            return true;
        }
    }
}
