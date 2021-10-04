namespace Quoridor.Model
{
    public class Pawn
    {
        public CellCoordinates CurrentCellCoordinates { get; private set; }

        public Pawn(CellCoordinates startPosition)
        {
            CurrentCellCoordinates = startPosition;
        }

        public void MoveToCell(CellCoordinates cellCoordinates)
        {
            CurrentCellCoordinates = cellCoordinates;
        }
    }
}
