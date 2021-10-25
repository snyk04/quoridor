namespace Quoridor.Model.Common
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

        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.row + b.row, a.column + b.column);
        }
        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.row - b.row, a.column - b.column);
        }

        public override string ToString()
        {
            return row + " " + column;
        }
    }
}
