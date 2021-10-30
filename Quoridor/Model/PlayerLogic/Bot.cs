using Quoridor.Model.Common;

namespace Quoridor.Model.PlayerLogic
{
    public abstract class Bot : Player
    {
        protected Bot(ModelCommunication model, PlayerColor playerColor, Coordinates startPosition, int startAmountOfWalls, int victoryRow)
            : base(model, playerColor, startPosition, startAmountOfWalls, victoryRow)
        {
        }
    }
}
