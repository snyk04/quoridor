using Quoridor.NewModel.Common;
using Quoridor.NewModel.PlayerLogic;

namespace Quoridor.NewModel
{
    public interface IModel
    {
        void StartNewGame(GameMode gameMode);
        void StopGame(GameStopType gameStopType);

        void MoveCurrentPlayerToCell(Coordinates cell);
        void PlaceCurrentPlayerWall(Coordinates wall);
    }
}
