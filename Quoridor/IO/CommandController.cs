using System;
using System.Collections.Generic;
using Quoridor.Model.Common;

namespace Quoridor.IO
{
    public static class CommandController
    {
        public static bool TryToReadCommand(out Command command, out string[] arguments)
        {
            command = default;
            arguments = default;
            
            string playerInput = Console.ReadLine();
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
        public static bool TryToConvert(Func<string, Coordinates> converter, IReadOnlyList<string> arguments, Command command, out Coordinates coordinates)
        {
            coordinates = default;
            
            try
            {
                coordinates = converter(arguments[0]);
            }
            catch
            {
                Console.WriteLine($"{command.ToString().ToLower()}: wrong argument");
                return false;
            }

            return true;
        }
        public static bool CheckArgumentsAmount(Command command, IReadOnlyCollection<string> arguments, int goalAmount)
        {
            if (arguments == null)
            {
                return false;
            }
            if (arguments.Count != goalAmount)
            {
                Console.WriteLine($"{command.ToString().ToLower()}: wrong amount of arguments");
                return false;
            }
            
            return true;
        }
    }
}
