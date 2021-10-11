namespace Quoridor.Model.Cells
{
    public readonly struct Coordinates
    {
        public readonly int row;
        public readonly int column;

        public Coordinates(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public override string ToString()
        {
            return row + " " + column;
        }
    }
}
