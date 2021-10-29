using Quoridor.Controller;

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
}
