namespace Quoridor.Model
{
    public readonly struct CellCoordinates
    {
        public readonly int row;
        public readonly int column;

        public CellCoordinates(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }
}
