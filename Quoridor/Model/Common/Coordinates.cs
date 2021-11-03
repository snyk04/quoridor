namespace Quoridor.Model.Common
{
    public readonly struct Coordinates
    {
        public readonly int Row;
        public readonly int Column;

        public Coordinates(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.Row + b.Row, a.Column + b.Column);
        }
        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.Row - b.Row, a.Column - b.Column);
        }

        public override string ToString()
        {
            return Row + " " + Column;
        }
    }
}
