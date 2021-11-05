using System.Collections.Generic;
using Quoridor.Model.Cells;
using Quoridor.Model.Common;

namespace Quoridor.Model.Pathfinding
{
    public class FieldPathFinder
    {
        private readonly ModelCommunication _model;

        public FieldPathFinder(ModelCommunication model)
        {
            _model = model;
        }

        public List<Coordinates> FindShortestPathToRow(Coordinates start, int row)
        {
            int[,] adjacencyMatrix = CalculateAdjacencyMatrix();
            
            return PathFinder.FindShortestPathToRow(start, row, adjacencyMatrix, CellsManager.AmountOfRows, CellsManager.AmountOfColumns);
        }

        private int[,] CalculateAdjacencyMatrix()
        {
            int matrixSize = CellsManager.AmountOfRows * CellsManager.AmountOfColumns;
            int[,] adjacencyMatrix = new int[matrixSize, matrixSize];

            for (int i = 0; i < CellsManager.AmountOfRows; i++)
            {
                for (int j = 0; j < CellsManager.AmountOfColumns; j++)
                {
                    AddNeighbours(i, j, adjacencyMatrix, CellsManager.AmountOfColumns);
                }
            }

            foreach (CellPair cellPair in _model.WallsManager.BlockedCellPairs)
            {
                BlockWay(cellPair[0], cellPair[1], adjacencyMatrix, CellsManager.AmountOfColumns);
            }

            return adjacencyMatrix;
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

        private static void AddWay(Coordinates cell1, Coordinates cell2, int[,] matrix, int amountOfColumns)
        {
            ConfigureConnection(cell1, cell2, 1, matrix, amountOfColumns);
        }
        private static void BlockWay(Coordinates cell1, Coordinates cell2, int[,] matrix, int amountOfColumns)
        {
            ConfigureConnection(cell1, cell2, 0, matrix, amountOfColumns);
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
