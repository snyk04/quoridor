using Quoridor.Model.Common;
using Quoridor.Model.PlayerLogic;

namespace Quoridor.Model
{
    public interface IModel
    {
        void StartNewGame(PlayerType whitePlayer, PlayerType blackPlayer);
        void StopGame(GameStopType gameStopType);

        void MoveCurrentPlayerToCell(Coordinates cell);
        void PlaceCurrentPlayerWall(Coordinates wall);
    }
}
