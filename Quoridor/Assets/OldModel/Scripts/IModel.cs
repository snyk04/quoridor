using Quoridor.OldModel.Cells;
using Quoridor.OldModel.Game;

namespace Quoridor.OldModel
{
    public interface IModel
    {
        void StartNewGame(GameMode gameMode);

        void MoveCurrentPlayerToCell(Coordinates cellCoordinates);
        void TryToPlaceWall(Coordinates wallCoordinates);
    }
}
