using Quoridor.Model.Cells;
using Quoridor.Model.Game;

namespace Quoridor.Model
{
    public interface IModel
    {
        void StartNewGame(GameMode gameMode);

        void MoveCurrentPlayerToCell(CellCoordinates cellCoordinates);
        void TryToPlaceWall(CellCoordinates wallCoordinates);
    }
}
