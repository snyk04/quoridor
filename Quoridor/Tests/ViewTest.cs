using System;
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
            ViewCommunication viewCommunication = new ViewCommunication();
            ModelCommunication modelCommunication = new ModelCommunication(viewCommunication);

            Player player = new Player(modelCommunication, PlayerType.First, new Coordinates(0, 0), 10, 0);
            
            viewCommunication.MovePlayerToCell(PlayerType.Second, new Coordinates(7, 4));
            viewCommunication.PlaceWall(player, new Coordinates(7, 4));
            viewCommunication.EndGame(PlayerType.First);
        }
    }
}