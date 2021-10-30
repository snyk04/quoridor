using Quoridor.Controller;
using Quoridor.Model.Common;
using Quoridor.Tests;

namespace Quoridor
{
    public static class Game
    {
        public static void Main(string[] args)
        {
            IController controller = new ControllerCommunication();

            controller.StartGame();
            
            // ConverterTest.TestCellsConverter(new Coordinates(0, 0), "A1");
            
            // ConverterTest.TestWallsConverter(new Coordinates(0, 0), "S1");
            // ConverterTest.TestWallsConverter(new Coordinates(1, 0), "S1h");
        }
    }
}
