using Quoridor.Model.Common;

namespace Quoridor.Model
{
    public class CellField
    {
        public Coordinates[] AvailableMoves(Coordinates playerCoordinates)
        {
            // TODO
            return new Coordinates[]
            {
                new Coordinates(playerCoordinates.row, playerCoordinates.column - 1),
                new Coordinates(playerCoordinates.row, playerCoordinates.column + 1)
            };
        }
    }
}
