namespace Quoridor.Model.Cells
{
    public class CellsManager
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

        public Cell GetCell(Coordinates cellCoordinates)
        {
            return Cells[cellCoordinates.row, cellCoordinates.column];
        }
        
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
            bool wallExists = false;
            CellPair cellPair = new CellPair(firstCell, secondCell);
            foreach (CellPair blockedCellPair in _model.WallsManager.BlockedCellPairs)
            {
                wallExists |= blockedCellPair.Equals(cellPair);
            }

            return wallExists;
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
