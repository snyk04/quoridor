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

            // ConvertorTest.TestCellsConvertor();
            // ConvertorTest.TestWallsConvertor();

            controller.StartGame();
        }
    }
    
    public static class ConvertorTest
    {
        public static void TestCellsConvertor()
        {
            Console.WriteLine($"C4 => 3 2| result = {new Coordinates(3, 2).Equals(CellsConverter.MixedToNumber("C4"))}");
            Console.WriteLine($"G6 => 5 6| result = {new Coordinates(5, 6).Equals(CellsConverter.MixedToNumber("G6"))}");
            Console.WriteLine($"1 2 => C2| result = {CellsConverter.NumberToMixed(new Coordinates(1, 2)).Equals("C2")}");
            Console.WriteLine($"6 3 => D7| result = {CellsConverter.NumberToMixed(new Coordinates(6, 3)).Equals("D7")}");
        }
        public static void TestWallsConvertor()
        {
            Console.WriteLine($"W4 => 6 4| result = {new Coordinates(6, 4).Equals(WallsConverter.MixedToNumber("W4"))}");
            Console.WriteLine($"T7h => 13 1| result = {new Coordinates(13, 1).Equals(WallsConverter.MixedToNumber("T7h"))}");
            Console.WriteLine($"0 3 => V1| result = {WallsConverter.NumberToMixed(new Coordinates(0, 3)).Equals("V1")}");
            Console.WriteLine($"15 5 => X8h| result = {WallsConverter.NumberToMixed(new Coordinates(15, 5)).Equals("X8h")}");
        }
    }
}
