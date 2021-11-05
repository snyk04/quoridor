using System;
using System.Collections.Generic;
using Quoridor.Model.Common;
using Quoridor.Model.Pathfinding;

namespace Quoridor.Tests
{
    public static class PathFindingTest
    {
        public static void TestPathFinder(int amountOfRows, int amountOfColumns)
        {
            int matrixSize = amountOfRows * amountOfColumns;
            int[,] matrix = new int[matrixSize, matrixSize];

            for (int i = 0; i < amountOfRows; i++)
            {
                for (int j = 0; j < amountOfColumns; j++)
                {
                    AddNeighbours(i, j, matrix, amountOfColumns);
                }
            }

            Coordinates start = new Coordinates(0, 0);
            int goalRow = 3;
            List<Coordinates> way = PathFinder.FindShortestPathToRow(start, goalRow, matrix, amountOfRows, amountOfColumns);
            if (way == null)
            {
                Console.WriteLine("no way!");
                return;
            }

            foreach (Coordinates nodeCoordinates in way)
            {
                Console.WriteLine(nodeCoordinates.ToString());
            }
        }

        private static void AddNeighbours(int row, int columns, int[,] matrix, int amountOfColumns)
        {
            var cell = new Coordinates(row, columns);
            if (row - 1 >= 0)
            {
                var neighbourCell = new Coordinates(row - 1, columns);
                AddWay(cell, neighbourCell, matrix, amountOfColumns);
            }
            if (columns + 1 < amountOfColumns)
            {
                var neighbourCell = new Coordinates(row, columns + 1);
                AddWay(cell, neighbourCell, matrix, amountOfColumns);
            }
            if (row + 1 < amountOfColumns)
            {
                var neighbourCell = new Coordinates(row + 1, columns);
                AddWay(cell, neighbourCell, matrix, amountOfColumns);
            }
            if (columns - 1 >= amountOfColumns)
            {
                var neighbourCell = new Coordinates(row, columns - 1);
                AddWay(cell, neighbourCell, matrix, amountOfColumns);
            }
        }

        private static void BlockWay(Coordinates cell1, Coordinates cell2, int[,] matrix, int amountOfColumns)
        {
            ConfigureConnection(cell1, cell2, 0, matrix, amountOfColumns);
        }
        private static void AddWay(Coordinates cell1, Coordinates cell2, int[,] matrix, int amountOfColumns)
        {
            ConfigureConnection(cell1, cell2, 1, matrix, amountOfColumns);
        }
        private static void ConfigureConnection(Coordinates cell1, Coordinates cell2, int connection, int[,] matrix, int amountOfColumns)
        {
            int firstCellIndex = FieldConverter.ToIndex(cell1, amountOfColumns);
            int secondCellIndex = FieldConverter.ToIndex(cell2, amountOfColumns);
            
            matrix[firstCellIndex, secondCellIndex] = connection;
            matrix[secondCellIndex, firstCellIndex] = connection;
        }
    }
}
