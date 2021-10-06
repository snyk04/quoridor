using Quoridor.Model.Cells;

namespace Quoridor.Model.Players
{
    public class Player
    {
        // public int AmountOfWalls { get; private set; }
        public CellCoordinates CurrentCellCoordinates { get; private set; }

        public Player(CellCoordinates startPosition) // int startAmountOfWalls
        {
            // AmountOfWalls = startAmountOfWalls;
            CurrentCellCoordinates = startPosition;
        }

        public void MoveToCell(CellCoordinates cellCoordinates)
        {
            CurrentCellCoordinates = cellCoordinates;
        }
    }
}
