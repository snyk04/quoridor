namespace Quoridor.Model
{
    public class Pawn
    {
        private CellCoordinates _currentCellCoordinates;
        public CellCoordinates CurrentCellCoordinates => _currentCellCoordinates;
        
        public Pawn(CellCoordinates startPosition)
        {
            _currentCellCoordinates = startPosition;
        }

        public void MoveToCell(CellCoordinates cellCoordinates)
        {
            _currentCellCoordinates = cellCoordinates;
        }
    }
}
