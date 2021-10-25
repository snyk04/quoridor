using Quoridor.Model.Common;

namespace Quoridor.Model.Cells
{
    public sealed class CellsManager
    {
        public const int AmountOfRows = 9;
        public const int AmountOfColumns = 9;
        
        private readonly ModelCommunication _model;

        public Cell[,] Cells { get; }

        public CellsManager(ModelCommunication model)
        {
            _model = model;
            
            Cells = new Cell[AmountOfRows, AmountOfColumns];
            
            InitializeCellField();
        }

        public Cell this[Coordinates cellCoordinates] => Cells[cellCoordinates.row, cellCoordinates.column];

        public bool CellIsBusy(Coordinates cell)
        {
            return Cells[cell.row, cell.column].IsBusy;
        }
        public bool CellIsReal(Coordinates cell)
        {
            return cell.row < AmountOfRows
                   & cell.row >= 0
                   & cell.column < AmountOfColumns
                   & cell.column >= 0; }
        public bool WallIsBetweenCells(Coordinates firstCell, Coordinates secondCell)
        {
            CellPair cellPair = new CellPair(firstCell, secondCell);
            
            foreach (CellPair blockedCellPair in _model.WallsManager.BlockedCellPairs)
            {
                if (blockedCellPair.Equals(cellPair))
                {
                    return true;
                }
            }

            return false;
        }
        
        private void InitializeCellField()
        {
            for (int i = 0; i < AmountOfRows; i++)
            {
                for (int j = 0; j < AmountOfColumns; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }
        }
    }
}
