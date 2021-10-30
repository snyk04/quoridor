using System;
using System.Collections.Generic;
using Quoridor.Model.Common;

namespace Quoridor.IO
{
    public static class CommandController
    {
        public static bool TryToConvert(Func<string, Coordinates> converter, IReadOnlyList<string> arguments, Command command, out Coordinates coordinates)
        {
            coordinates = default;
            
            try
            {
                coordinates = converter(arguments[0]);
            }
            catch (Exception e)
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
