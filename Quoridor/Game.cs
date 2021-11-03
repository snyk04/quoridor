using System;
using Quoridor.Tests;

namespace Quoridor
{
    public static class Game
    {
        public static void Main(string[] args)
        {
            // IController controller = new ControllerCommunication();
            // controller.StartGame();
            PathFindingTest.TestPathFinder(4);

            // ConverterTester.Test(new Coordinates(3, 3), 15, 4, 4);
        }
    }
}
