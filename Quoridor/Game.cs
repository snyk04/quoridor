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
            
            // ConverterTest.TestWallsConverter(new Coordinates(0, 0), "S1v");
            // ConverterTest.TestWallsConverter(new Coordinates(1, 0), "S1h");
            
            // ViewTest.Test();
        }
    }
}
