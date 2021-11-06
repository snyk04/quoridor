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

        public List<Coordinates> FindShortestPathToRow(Coordinates start, int row, IEnumerable<Coordinates> jumps, Coordinates? enemyCell)
        {
            int[,] adjacencyMatrix = CalculateAdjacencyMatrix(start, jumps, enemyCell);
            
            return PathFinder.FindShortestPathToRow(start, row, adjacencyMatrix, CellsManager.AmountOfRows, CellsManager.AmountOfColumns);
        }

        private int[,] CalculateAdjacencyMatrix(Coordinates start, IEnumerable<Coordinates> jumps, Coordinates? enemyCell)
        {
            int matrixSize = CellsManager.AmountOfRows * CellsManager.AmountOfColumns;
            int[,] adjacencyMatrix = new int[matrixSize, matrixSize];

            for (int i = 0; i < CellsManager.AmountOfRows; i++)
            {
                for (int j = 0; j < CellsManager.AmountOfColumns; j++)
                {
                    AddNeighbours(i, j, adjacencyMatrix);
                }
            }

            foreach (CellPair cellPair in _model.WallsManager.BlockedCellPairs)
            {
                BlockWay(cellPair[0], cellPair[1], adjacencyMatrix);
            }

            foreach (Coordinates jump in jumps)
            {
                AddWay(start, jump, adjacencyMatrix);
            }

            if (enemyCell is Coordinates cell)
            {
                MakeCellUnavailable(cell.Row, cell.Column, adjacencyMatrix);
            }
            
            return adjacencyMatrix;
        }
        
        private static void AddNeighbours(int row, int column, int[,] matrix)
        {
            var cell = new Coordinates(row, column);
            if (row - 1 >= 0)
            {
                var neighbourCell = new Coordinates(row - 1, column);
                AddWay(cell, neighbourCell, matrix);
            }
            if (column + 1 < CellsManager.AmountOfColumns)
            {
                var neighbourCell = new Coordinates(row, column + 1);
                AddWay(cell, neighbourCell, matrix);
            }
            if (row + 1 < CellsManager.AmountOfRows)
            {
                var neighbourCell = new Coordinates(row + 1, column);
                AddWay(cell, neighbourCell, matrix);
            }
            if (column - 1 >= 0)
            {
                var neighbourCell = new Coordinates(row, column - 1);
                AddWay(cell, neighbourCell, matrix);
            }
        }

        private static void MakeCellUnavailable(int row, int columns, int[,] matrix)
        {
            var cell = new Coordinates(row, columns);
            if (row - 1 >= 0)
            {
                var neighbourCell = new Coordinates(row - 1, columns);
                BlockWay(cell, neighbourCell, matrix);
            }
            if (columns + 1 < CellsManager.AmountOfColumns)
            {
                var neighbourCell = new Coordinates(row, columns + 1);
                BlockWay(cell, neighbourCell, matrix);
            }
            if (row + 1 < CellsManager.AmountOfRows)
            {
                var neighbourCell = new Coordinates(row + 1, columns);
                BlockWay(cell, neighbourCell, matrix);
            }
            if (columns - 1 >= 0)
            {
                var neighbourCell = new Coordinates(row, columns - 1);
                BlockWay(cell, neighbourCell, matrix);
            }
        }

        private static void AddWay(Coordinates cell1, Coordinates cell2, int[,] matrix)
        {
            ConfigureConnection(cell1, cell2, 1, matrix);
        }
        private static void BlockWay(Coordinates cell1, Coordinates cell2, int[,] matrix)
        {
            ConfigureConnection(cell1, cell2, 0, matrix);
        }
        private static void ConfigureConnection(Coordinates cell1, Coordinates cell2, int connection, int[,] matrix)
        {
            int firstCellIndex = FieldConverter.ToIndex(cell1, CellsManager.AmountOfColumns);
            int secondCellIndex = FieldConverter.ToIndex(cell2, CellsManager.AmountOfColumns);
            
            matrix[firstCellIndex, secondCellIndex] = connection;
            matrix[secondCellIndex, firstCellIndex] = connection;
        }
    }
}
