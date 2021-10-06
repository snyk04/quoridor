namespace Quoridor.Model.Cells
{
    public class CellsManager
    {
        public const int AmountOfRows = 9;
        public const int AmountOfColumns = 9;
        
        public Cell[,] Cells { get; }

        public CellsManager()
        {
            Cells = new Cell[AmountOfRows, AmountOfColumns];
            
            InitializeCellField();
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

        public bool CheckIfCellIsReal(CellCoordinates cellCoordinates)
        {
            return cellCoordinates.row < AmountOfRows 
                   & cellCoordinates.row >= 0 
                   & cellCoordinates.column < AmountOfColumns 
                   & cellCoordinates.column >= 0;
        }
        public bool CheckIfCellIsBusy(CellCoordinates cellCoordinates)
        {
            return Cells[cellCoordinates.row, cellCoordinates.column].IsBusy;
        }
    }
}
