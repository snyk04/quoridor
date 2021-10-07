using Quoridor.Model.Cells;

namespace Quoridor.Model.Players
{
    public class Player
    {
        public int AmountOfWalls { get; private set; }
        public CellCoordinates CurrentCellCoordinates { get; private set; }

        private readonly int _defaultAmountOfWalls;

        public Player(int startAmountOfWalls, CellCoordinates startPosition)
        {
            AmountOfWalls = startAmountOfWalls;
            CurrentCellCoordinates = startPosition;

            _defaultAmountOfWalls = startAmountOfWalls;
        }

        public void MoveToCell(CellCoordinates cellCoordinates)
        {
            CurrentCellCoordinates = cellCoordinates;
        }

        public void ResetAmountOfWallsToDefault()
        {
            AmountOfWalls = _defaultAmountOfWalls;
        }
        public void PlaceWall()
        {
            AmountOfWalls -= 1;
        }
    }
}
