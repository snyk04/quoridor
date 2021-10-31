using Quoridor.Controller;
using Quoridor.Model;
using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;
using Quoridor.View;

namespace Quoridor.Tests
{
    public static class ViewTest
    {
        public static void Test()
        {
            ControllerCommunication controllerCommunication = new ControllerCommunication();
            ViewCommunication viewCommunication = new ViewCommunication(controllerCommunication);
            ModelCommunication modelCommunication = new ModelCommunication(viewCommunication);

            Player player = new Player(modelCommunication, PlayerColor.White, PlayerType.Player1, new Coordinates(0, 0), 10, 0);
            
            viewCommunication.MovePlayerToCell(player, new Coordinates(7, 4));
            viewCommunication.JumpPlayerToCell(player, new Coordinates(4, 3));
            viewCommunication.PlaceWall(player, new Coordinates(7, 4));
            viewCommunication.EndGame(PlayerColor.White);
        }
    }
}
