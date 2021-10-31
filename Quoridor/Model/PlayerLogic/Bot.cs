using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public abstract class Bot : Player
    {
        protected Bot(ModelCommunication model, PlayerColor playerColor, PlayerType playerType, Coordinates startPosition, int startAmountOfWalls, int victoryRow)
            : base(model, playerColor, playerType, startPosition, startAmountOfWalls, victoryRow)
        {
        }
    }
}
