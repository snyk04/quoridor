namespace Quoridor.Model.Players
{
    public class Player
    {
        public CellCoordinates CurrentCellCoordinates { get; private set; }
        
        public Player(CellCoordinates startPosition)
        {
            CurrentCellCoordinates = startPosition;
        }

        public void MoveToCell(CellCoordinates cellCoordinates)
        {
            CurrentCellCoordinates = cellCoordinates;
        }
    }
}
