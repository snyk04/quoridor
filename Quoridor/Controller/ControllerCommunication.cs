using System;

namespace Quoridor.Controller
{
    public class ControllerCommunication : IController
    {
        public void StartGame()
        {
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
