using System;

namespace Quoridor.Model.Common
{
    public static class FieldConverter
    {
        public static int ToIndex(Coordinates coordinates, int amountOfColumns)
        {
            return coordinates.row * amountOfColumns + coordinates.column;
        }
        public static Coordinates ToCoordinates(int index, int amountOfRows, int amountOfColumns)
        {
            int row = (int) Math.Floor((double) index / amountOfRows);
            int column = index % amountOfColumns;
            
            return new Coordinates(row, column);
        }
    }
}
