using System;
using System.Collections.Generic;

namespace Quoridor.Controller
{
    public static class CustomConsole
    {
        public static void WriteLine(string line)
        {
            Console.WriteLine("<- " + line);
        }
        public static bool TryToReadCommand(out Command command, out string[] arguments)
        {
            command = default;
            arguments = default;
            
            string playerInput = ReadLine();
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

        private static string ReadLine()
        {
            Console.Write("-> ");
            return Console.ReadLine();
        }
    }
}
