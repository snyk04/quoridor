using System;
using Quoridor.Controller;
using Quoridor.Model.Common;

namespace Quoridor
{
    public static class Game
    {
        public static void Main(string[] args)
        {
            IController controller = new ControllerCommunication();

            controller.StartGame();
        }
    }
    
    public static class ConverterTest
    {
        public static void TestCellsConverter(Coordinates numberCoordinates, string mixedCoordinates)
        {
            Console.WriteLine($"{mixedCoordinates} => {numberCoordinates.ToString()} | " +
                              $"result = {numberCoordinates.Equals(CellsConverter.MixedToNumber(mixedCoordinates))}");
            Console.WriteLine($"{numberCoordinates.ToString()} => {mixedCoordinates}| " +
                              $"result = {CellsConverter.NumberToMixed(numberCoordinates).Equals(mixedCoordinates)}");
        }
        public static void TestWallsConverter(Coordinates numberCoordinates, string mixedCoordinates)
        {
            Console.WriteLine($"{mixedCoordinates} => {numberCoordinates.ToString()} | " +
                              $"result = {numberCoordinates.Equals(WallsConverter.MixedToNumber(mixedCoordinates))}");
            Console.WriteLine($"{numberCoordinates.ToString()} => {mixedCoordinates}| " +
                              $"result = {WallsConverter.NumberToMixed(numberCoordinates).Equals(mixedCoordinates)}");
        }
    }
}
