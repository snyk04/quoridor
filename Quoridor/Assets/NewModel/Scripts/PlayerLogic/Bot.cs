using Quoridor.NewModel.Common;

namespace Quoridor.NewModel.PlayerLogic
{
    public abstract class Bot : Player
    {
        protected Bot(ModelCommunication model, PlayerType playerType, Coordinates startPosition, int startAmountOfWalls)
            : base(model, playerType, startPosition, startAmountOfWalls)
        {
        }
    }
}
