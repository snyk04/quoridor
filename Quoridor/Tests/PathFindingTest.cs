using System;
using System.Collections.Generic;
using System.Diagnostics;
using Quoridor.Model.Common;
using Quoridor.Model.Pathfinding;

namespace Quoridor.Tests
{
    public static class PathFindingTest
    {
        public static void TestPathFinder(int fieldSize)
        {
            int matrixSize = fieldSize * fieldSize;
            int[,] matrix = new int[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    matrix[i, j] = 1;
                }
            }

            BlockWay(new Coordinates(0, 0), new Coordinates(1, 0), matrix);

            Coordinates start = new Coordinates(0, 0);
            Coordinates goal = new Coordinates(3, 3);
            List<Coordinates> way = PathFinder.FindPath(start, goal, matrix, 4, 4);
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

        private static void BlockWay(Coordinates cell1, Coordinates cell2, int[,] matrix)
        {
            matrix[FieldConverter.ToIndex(cell1, 4), FieldConverter.ToIndex(cell2, 4)] = 0;
            matrix[FieldConverter.ToIndex(cell2, 4), FieldConverter.ToIndex(cell1, 4)] = 0;
        }
    }
}
